using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features._subject.Commands.DeactivateSubjectCommands
{
    public class DeactivateSubjectCommand : IRequest<Response<Guid>>
    {
        public int Id { get; set; }

        public DeactivateSubjectCommand(int id)
        {
            Id = id;
        }
    }
    public class DeactivateSubjectCommandHandler : IRequestHandler<DeactivateSubjectCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;

        public DeactivateSubjectCommandHandler(IRepositoryAsync<Subject> subjectRepositoryAsync)
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeactivateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (subject == null)
                return new Response<Guid>($"Subject with ID {request.Id} not found");

            if (!subject.IsActive)
                return new Response<Guid>($"Subject with ID {request.Id} is already inactive");

            subject.IsActive = false;
            await _subjectRepositoryAsync.UpdateAsync(subject, cancellationToken);

            return new Response<Guid>(subject.Id, "Subject succesfully deactivated");
        }
    }
}
