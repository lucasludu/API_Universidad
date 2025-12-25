using Application.Interfaces;
using Application.Specification._career;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.DTOs._career.Request;

namespace Application.Features._career.Commands.AddCareerCommands
{
    public record AddCareerCommand(CareerInsertRequest Request) : IRequest<Response<Guid>>;

    public class AddCareerCommandHandler : IRequestHandler<AddCareerCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;
        private readonly IMapper _mapper;

        public AddCareerCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync, IMapper mapper)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(AddCareerCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetCareerByNameSpec(request.Request.Name);
            var exists = await _careerRepositoryAsync.AnyAsync(spec, cancellationToken);

            if (exists)
                return new Response<Guid>($"A career with this name ({request.Request.Name}) already exists");

            var career = _mapper.Map<Career>(request.Request);
            var result = await _careerRepositoryAsync.AddAsync(career, cancellationToken);

            return new Response<Guid>(result.Id, "Career created successfully");
        }
    }
}
