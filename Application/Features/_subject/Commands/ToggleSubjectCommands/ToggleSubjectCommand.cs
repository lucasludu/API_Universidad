using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features._subject.Commands.ToggleSubjectCommands
{
    public class ToggleSubjectCommand : IRequest<Response<Guid>>
    {
        public int Id { get; set; }

        public ToggleSubjectCommand(int id)
        {
            Id = id;
        }
    }

    public class ToggleSubjectCommandHandler : IRequestHandler<ToggleSubjectCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;

        public ToggleSubjectCommandHandler(IRepositoryAsync<Subject> subjectRepositoryAsync)
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(ToggleSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (subject == null) 
                return new Response<Guid>($"Subject with ID {request.Id} not found");

            subject.IsActive  = !subject.IsActive;
            await _subjectRepositoryAsync.UpdateAsync(subject, cancellationToken);

            var status = subject.IsActive ? "reactivated" : "deactivated";
            return new Response<Guid>(subject.Id, $"Subject successfully {status}");
        }
    }
}
