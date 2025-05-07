using Application.Feautures._career_subject.Commands.AddCareerSubjectCommands;
using Application.Interfaces;
using Application.Parameters;
using Application.Specification._career;
using Application.Specification._career_subject;
using Application.Specification._subject;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautures._career_subject.Commands.AddCareerSubjectsBatchCommands
{
    public class AddCareerSubjectsBatchCommand : IRequest<Response<BatchSubjectInsertResult>>
    {
        public List<AddCareerSubjectCommand> SubjectsToAssign { get; set; }
    }

    public class AddCareerSubjectsBatchCommandHandler : IRequestHandler<AddCareerSubjectsBatchCommand, Response<BatchSubjectInsertResult>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;
        private readonly IRepositoryAsync<CareerSubject> _careerSubjectRepositoryAsync;
        private readonly IApplicationDbContext _context;

        public AddCareerSubjectsBatchCommandHandler(
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

        public async Task<Response<BatchSubjectInsertResult>> Handle(AddCareerSubjectsBatchCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            var result = new BatchSubjectInsertResult();

            try
            {
                foreach (var item in request.SubjectsToAssign)
                {
                    try
                    {
                        var subjectSpec = new SubjectByNameSpec(item.Request.SubjectName);
                        var subject = await _subjectRepositoryAsync.FirstOrDefaultAsync(subjectSpec, cancellationToken);
                        if (subject == null)
                        {
                            subject = new Subject
                            {
                                Name = item.Request.SubjectName,
                                Description = item.Request.SubjectDescription
                            };
                            await _subjectRepositoryAsync.AddAsync(subject, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }

                        var careerSpec = new GetCareerByNameSpec(item.Request.CareerName);
                        var career = await _careerRepositoryAsync.FirstOrDefaultAsync(careerSpec, cancellationToken);
                        if (career == null)
                        {
                            result.Errors.Add($"Career '{item.Request.CareerName}' not found for subject '{item.Request.SubjectName}'.");
                            continue;
                        }

                        var careerSubjectSpec = new GetRelationCareerSubjectSpec(career.Id, subject.Id);
                        var existsCS = await _careerSubjectRepositoryAsync.AnyAsync(careerSubjectSpec, cancellationToken);
                        if (existsCS)
                        {
                            result.Errors.Add($"Subject '{item.Request.SubjectName}' is already assigned to career '{item.Request.CareerName}'.");
                            continue;
                        }

                        var cs = new CareerSubject
                        {
                            CareerId = career.Id,
                            SubjectId = subject.Id,
                            Year = item.Request.Year,
                            Semester = item.Request.Semester
                        };
                        await _careerSubjectRepositoryAsync.AddAsync(cs, cancellationToken);
                        result.InsertedCount++;
                    }
                    catch (Exception innerEx)
                    {
                        result.Errors.Add($"Error with subject '{item.Request.SubjectName}': {innerEx.Message}");
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new Response<BatchSubjectInsertResult>(result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new Response<BatchSubjectInsertResult>($"Transaction failed: {ex.Message}");
            }

        }
    }
}
