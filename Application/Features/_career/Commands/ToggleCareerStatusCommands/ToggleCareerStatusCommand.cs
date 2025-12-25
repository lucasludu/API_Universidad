using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features._career.Commands.ToggleCareerStatusCommands
{
    public class ToggleCareerStatusCommand : IRequest<Response<Guid>>
    {
        public int Id { get; set; }

        public ToggleCareerStatusCommand(int id)
        {
            Id = id;
        }
    }

    public class ToggleCareerStatusCommandHandler : IRequestHandler<ToggleCareerStatusCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;

        public ToggleCareerStatusCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(ToggleCareerStatusCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (career == null)
                return new Response<Guid>($"Career with ID {request.Id} not found");

            career.IsActive = !career.IsActive;
            await _careerRepositoryAsync.UpdateAsync(career, cancellationToken);

            var status = career.IsActive ? "reactivated" : "deactivated";
            return new Response<Guid>(career.Id, $"Career successfully {status}");
        }
    }

}
