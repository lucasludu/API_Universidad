using Application.Interfaces;
using Application.Specification._career;
using Application.Specification._career_subject;
using Application.Specification._subject;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Application.DTOs._career_subject.Request;

namespace Application.Features._career_subject.Commands.AddCareerSubjectCommands
{
    public class AddCareerSubjectCommand : IRequest<Response<string>>
    {
        public CareerSubjectRequest Request { get; set; }

        public AddCareerSubjectCommand(CareerSubjectRequest request)
        {
            Request = request;
        }
    }

    public class AddCareerSubjectCommandHandler : IRequestHandler<AddCareerSubjectCommand, Response<string>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;
        private readonly IRepositoryAsync<CareerSubject> _careerSubjectRepositoryAsync;
        private readonly IApplicationDbContext _context;

        public AddCareerSubjectCommandHandler(
                IRepositoryAsync<Career> careerRepositoryAsync,
                IRepositoryAsync<Subject> subjectRepositoryAsync,
                IRepositoryAsync<CareerSubject> careerSubjectRepositoryAsync,
                IApplicationDbContext context
            )
        {
            _careerRepositoryAsync = careerRepositoryAsync;
            _subjectRepositoryAsync = subjectRepositoryAsync;
            _careerSubjectRepositoryAsync = careerSubjectRepositoryAsync;
            _context = context;
        }

        public async Task<Response<string>> Handle(AddCareerSubjectCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            int addedCount = 0;
            try
            {
                // Buscar o crear la materia
                var subjectSpec = new SubjectByNameSpec(request.Request.SubjectName);
                var subject = await _subjectRepositoryAsync.FirstOrDefaultAsync(subjectSpec, cancellationToken);
                if(subject == null)
                {
                    subject = new Subject
                    {
                        Name = request.Request.SubjectName,
                        Description = request.Request.SubjectDescription
                    };
                    await _subjectRepositoryAsync.AddAsync(subject, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Buscar la carrera por nombre
                var careerSpec = new GetCareerByNameSpec(request.Request.CareerName);
                var career = await _careerRepositoryAsync.FirstOrDefaultAsync(careerSpec, cancellationToken);
                if (career == null)
                {
                    return new Response<string>($"Career '{request.Request.CareerName}' not found");
                }

                // Verifico que exista la relación
                var careerSubjectSpec = new GetRelationCareerSubjectSpec(career.Id, subject.Id);
                var existCareerSubject = await _careerSubjectRepositoryAsync.AnyAsync(careerSubjectSpec, cancellationToken);
                if(existCareerSubject)
                    return new Response<string>($"Career '{career.Name}' already has the subject '{subject.Name}'");

                // Crear la relación
                var careerSubject = new CareerSubject
                {
                    CareerId = career.Id,
                    SubjectId = subject.Id,
                    Year = request.Request.Year,
                    Semester = request.Request.Semester
                };
                await _careerSubjectRepositoryAsync.AddAsync(careerSubject, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return new Response<string>($"Subject '{subject.Name}' assigned to career '{career.Name}'");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new Response<string>(ex.Message);
            }
        }
    }
}
