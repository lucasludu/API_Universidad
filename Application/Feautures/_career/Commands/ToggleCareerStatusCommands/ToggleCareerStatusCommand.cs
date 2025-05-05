using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Feautures._career.Commands.ToggleCareerStatusCommands
{
    public class ToggleCareerStatusCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public ToggleCareerStatusCommand(int id)
        {
            Id = id;
        }
    }

    public class ToggleCareerStatusCommandHandler : IRequestHandler<ToggleCareerStatusCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;

        public ToggleCareerStatusCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
        }

        public async Task<Response<int>> Handle(ToggleCareerStatusCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (career == null)
                return new Response<int>($"Career with ID {request.Id} not found");

            career.IsActive = !career.IsActive;
            await _careerRepositoryAsync.UpdateAsync(career, cancellationToken);

            var status = career.IsActive ? "reactivated" : "deactivated";
            return new Response<int>(career.Id, $"Career successfully {status}");
        }
    }

}
