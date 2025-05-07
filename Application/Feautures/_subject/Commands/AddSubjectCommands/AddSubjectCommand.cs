using Application.Interfaces;
using Application.Specification._career;
using Application.Specification._subject;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Models._subject.Request;

namespace Application.Feautures._subject.Commands.AddSubjectCommands
{
    public class AddSubjectCommand : IRequest<Response<int>>
    {
        public SubjectInsertRequest Request { get; set; }

        public AddSubjectCommand(SubjectInsertRequest request)
        {
            Request = request;
        }
    }
    public class AddSubjectCommandHandler : IRequestHandler<AddSubjectCommand, Response<int>>
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

        public async Task<Response<int>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectSpec = new SubjectByNameSpec(request.Request.Name);
            var subjectExists = await _subjectRepositoryAsync.AnyAsync(subjectSpec, cancellationToken);
            if(subjectExists)
                return new Response<int>($"A subject with this name ({request.Request.Name}) already exists in this career");

            var subject = _mapper.Map<Subject>(request.Request);
            await _subjectRepositoryAsync.AddAsync(subject, cancellationToken);

            return new Response<int>(subject.Id, "Subject created successfully");
        }
    }
}
