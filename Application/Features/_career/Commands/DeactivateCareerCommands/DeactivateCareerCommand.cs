using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features._career.Commands.DeactivateCareerCommands
{
    public class DeactivateCareerCommand : IRequest<Response<Guid>>
    {
        public int Id { get; set; }

        public DeactivateCareerCommand(int id)
        {
            Id = id;
        }
    }

    public class DeactivateCareerCommandHandler : IRequestHandler<DeactivateCareerCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;

        public DeactivateCareerCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeactivateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (career == null)
                return new Response<Guid>($"Career with ID {request.Id} not found");

            if (!career.IsActive)
                return new Response<Guid>($"Career with ID {request.Id} is already inactive");

            career.IsActive = false;
            await _careerRepositoryAsync.UpdateAsync(career, cancellationToken);

            return new Response<Guid>(career.Id, "Career successfully deactivated");
        }
    }
}
