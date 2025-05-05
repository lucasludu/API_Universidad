using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautures._subject.Commands.EnableSubjectCommands
{
    public class EnableSubjectCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public EnableSubjectCommand(int id)
        {
            Id = id;
        }
    }
    public class EnableSubjectCommandHandler : IRequestHandler<EnableSubjectCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;

        public EnableSubjectCommandHandler(IRepositoryAsync<Subject> subjectRepositoryAsync)
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
        }

        public async Task<Response<int>> Handle(EnableSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if(subject == null) 
                return new Response<int>($"Subject with ID {request.Id} not found");

            if (subject.IsActive)
                return new Response<int>($"Subject with ID {request.Id} is already active");

            subject.IsActive = true;
            await _subjectRepositoryAsync.UpdateAsync(subject, cancellationToken);

            return new Response<int>(subject.Id, "Subject succesfully reactivated");
        }
    }
}
