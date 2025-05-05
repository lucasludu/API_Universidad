using Application.Interfaces;
using Application.Specification._career;
using Application.Specification._subject;
using Application.Wrappers;
using ClosedXML.Excel;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Feautures._subject.Commands.UploadSubjectsCommands
{
    public class UploadSubjectsCommand : IRequest<Response<string>>
    {
        public int CareerId { get; set; }
        public IFormFile File { get; set; }

        public UploadSubjectsCommand(int careerId, IFormFile file)
        {
            CareerId = careerId;
            File = file;
        }
    }
    public class UploadSubjectsCommandHandler : IRequestHandler<UploadSubjectsCommand, Response<string>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepository;
        private readonly IRepositoryAsync<Career> _careerRepository;

        public UploadSubjectsCommandHandler(IRepositoryAsync<Subject> subjectRepository, IRepositoryAsync<Career> careerRepository)
        {
            _subjectRepository = subjectRepository;
            _careerRepository = careerRepository;
        }

        public async Task<Response<string>> Handle(UploadSubjectsCommand request, CancellationToken cancellationToken)
        {
            var careerExists = await _careerRepository.AnyAsync(new GetCareerByIdSpec(request.CareerId), cancellationToken);
            if(!careerExists)
                return new Response<string>($"Career with ID {request.CareerId} does not exist");

            int added = 0, skipped = 0;

            using var stream = new MemoryStream();
            await request.File.CopyToAsync(stream, cancellationToken);

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            var rows = worksheet.RowsUsed().Skip(1); // Skip header row

            foreach(var row in rows)
            {
                var name = row.Cell(1).GetString();
                var description = row.Cell(2).GetString();
                var year = row.Cell(3).GetString();
                var semester = row.Cell(4).GetString();

                var spec = new SubjectByNameAndCareerIdSpec(name, request.CareerId);
                var exists = await _subjectRepository.AnyAsync(spec, cancellationToken);
                if (exists)
                {
                    skipped++;
                    continue;
                }

                var subject = new Subject
                {
                    Name = name,
                    Description = string.IsNullOrWhiteSpace(description) ? null : description,
                    CareerId = request.CareerId,
                    Year = int.TryParse(year, out var parsedYear) ? parsedYear : 0,
                    Semester = int.TryParse(semester, out var parsedSemester) ? parsedSemester : 0
                };

                await _subjectRepository.AddAsync(subject, cancellationToken);
                added++;
            }
            return new Response<string>($"Added {added} subjects and skipped {skipped} subjects.");
        }
    }
}
