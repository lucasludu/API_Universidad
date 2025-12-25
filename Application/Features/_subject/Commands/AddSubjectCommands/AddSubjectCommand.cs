using Application.Interfaces;
using Application.Specification._career;
using Application.Specification._subject;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs._subject.Request;

namespace Application.Features._subject.Commands.AddSubjectCommands
{
    public class AddSubjectCommand : IRequest<Response<Guid>>
    {
        public SubjectInsertRequest Request { get; set; }

        public AddSubjectCommand(SubjectInsertRequest request)
        {
            Request = request;
        }
    }
    public class AddSubjectCommandHandler : IRequestHandler<AddSubjectCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;
        private readonly IMapper _mapper;

        public AddSubjectCommandHandler (
                IRepositoryAsync<Subject> subjectRepositoryAsync, 
                IMapper mapper
            )
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectSpec = new SubjectByNameSpec(request.Request.Name);
            var subjectExists = await _subjectRepositoryAsync.AnyAsync(subjectSpec, cancellationToken);
            if(subjectExists)
                return new Response<Guid>($"A subject with this name ({request.Request.Name}) already exists in this career");

            var subject = _mapper.Map<Subject>(request.Request);
            await _subjectRepositoryAsync.AddAsync(subject, cancellationToken);

            return new Response<Guid>(subject.Id, "Subject created successfully");
        }
    }
}
