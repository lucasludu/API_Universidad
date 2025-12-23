using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features._career.Commands.EnableCareerCommands
{
    public class EnableCareerCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public EnableCareerCommand(int id)
        {
            Id = id;
        }
    }

    public class EnableCareerCommandHandler : IRequestHandler<EnableCareerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Career> _careerRepositoryAsync;

        public EnableCareerCommandHandler(IRepositoryAsync<Career> careerRepositoryAsync)
        {
            _careerRepositoryAsync = careerRepositoryAsync;
        }

        public async Task<Response<int>> Handle(EnableCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await _careerRepositoryAsync.GetByIdAsync(request.Id, cancellationToken);
            if (career == null)
                return new Response<int>($"Career with ID {request.Id} not found");

            if (career.IsActive)
                return new Response<int>($"Career with ID {request.Id} is already active");

            career.IsActive = true;
            await _careerRepositoryAsync.UpdateAsync(career, cancellationToken);

            return new Response<int>(career.Id, "Career successfully reactivated");
        }
    }

}
