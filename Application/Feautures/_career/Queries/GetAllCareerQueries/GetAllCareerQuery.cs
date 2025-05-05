using Application.Interfaces;
using Application.Specification._career;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Models._career.Response;

namespace Application.Feautures._career.Queries.GetAllCareerQueries
{
    public class GetAllCareerQuery : IRequest<PagedResponse<List<CareerResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
    }
    public class GetAllCareerQueryHandler : IRequestHandler<GetAllCareerQuery, PagedResponse<List<CareerResponse>>>
    {
        private readonly IRepositoryAsync<Career> _careerRepository;
        private readonly IMapper _mapper;

        public GetAllCareerQueryHandler(IRepositoryAsync<Career> careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<CareerResponse>>> Handle(GetAllCareerQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetPagedCareerSpec(request.PageSize, request.PageNumber, request.Name, request.IsActive);

            var careers = await _careerRepository.ListAsync(new GetPagedCareerSpec(request.PageSize, request.PageNumber), cancellationToken);

            var totalCount = await _careerRepository.CountAsync(spec, cancellationToken);

            var careerResponse = _mapper.Map<List<CareerResponse>>(careers);

            var pagedResponse = new PagedResponse<List<CareerResponse>>(careerResponse, request.PageNumber, request.PageSize, totalCount);

            return pagedResponse;
        }
    }
}
