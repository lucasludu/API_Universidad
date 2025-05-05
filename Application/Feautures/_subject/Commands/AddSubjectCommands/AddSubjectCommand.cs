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
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;
        private readonly IMapper _mapper;

        public AddSubjectCommandHandler(IRepositoryAsync<Subject> subjectRepositoryAsync, IRepositoryAsync<Career> careerRepositoryAsync, IMapper mapper)
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
            _careerRepositoryAsync = careerRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var careerSpec = new GetCareerByIdSpec(request.Request.CareerId);
            var careerExists = await _careerRepositoryAsync.AnyAsync(careerSpec, cancellationToken);
            if (!careerExists)
                return new Response<int>($"A career with this id ({request.Request.CareerId}) does not exist");

            var subjectSpec = new SubjectByNameAndCareerIdSpec(request.Request.Name, request.Request.CareerId);
            var subjectExists = await _subjectRepositoryAsync.AnyAsync(subjectSpec, cancellationToken);
            if(subjectExists)
                return new Response<int>($"A subject with this name ({request.Request.Name}) already exists in this career");

            var subject = _mapper.Map<Subject>(request.Request);
            await _subjectRepositoryAsync.AddAsync(subject, cancellationToken);

            return new Response<int>(subject.Id, "Subject created successfully");
        }
    }
}
