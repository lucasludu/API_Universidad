using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautures._career.Commands.DeactivateCareerCommands
{
    public class DeactivateCareerCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public DeactivateCareerCommand(int id)
        {
            Id = id;
        }
    }

    public class DeactivateCareerCommandHandler : IRequestHandler<DeactivateCareerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;

        public DeactivateCareerCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
        }

        public async Task<Response<int>> Handle(DeactivateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (career == null)
                return new Response<int>($"Career with ID {request.Id} not found");

            if (!career.IsActive)
                return new Response<int>($"Career with ID {request.Id} is already inactive");

            career.IsActive = false;
            await _careerRepositoryAsync.UpdateAsync(career, cancellationToken);

            return new Response<int>(career.Id, "Career successfully deactivated");
        }
    }
}
