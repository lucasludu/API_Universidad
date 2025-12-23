using Application.Interfaces;
using Application.Specification._subject;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs._subject.Response;

namespace Application.Features._subject.Queries.GetAllSubjectQueries
{
    public class GetAllSubjectQuery : IRequest<PagedResponse<List<SubjectResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetAllSubjectQueryHandler : IRequestHandler<GetAllSubjectQuery, PagedResponse<List<SubjectResponse>>>
    {
        private readonly IRepositoryAsync<Subject> _subjectRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllSubjectQueryHandler(IRepositoryAsync<Subject> subjectRepositoryAsync, IMapper mapper)
        {
            _subjectRepositoryAsync = subjectRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<SubjectResponse>>> Handle(GetAllSubjectQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetPagedSubjectSpec(request.PageSize, request.PageNumber, request.Name, request.IsActive);

            var subjects = await _subjectRepositoryAsync.ListAsync(spec, cancellationToken);

            var totalCount = await _subjectRepositoryAsync.CountAsync(spec, cancellationToken);

            var subjectResponse = _mapper.Map<List<SubjectResponse>>(subjects);

            var pagedResponse = new PagedResponse<List<SubjectResponse>>(subjectResponse, request.PageNumber, request.PageSize, totalCount);

            return pagedResponse;
        }
    }
}
