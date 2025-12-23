using Application.Features._career.Commands.AddCareerCommands;
using Application.Features._career.Commands.DeactivateCareerCommands;
using Application.Features._career.Commands.EnableCareerCommands;
using Application.Features._career.Commands.ToggleCareerStatusCommands;
using Application.Features._career.Queries.GetAllCareerQueries;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs._career.Request;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class CareerController : BaseApiController
    {
        /// <summary>
        /// Add a new career
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCareer([FromBody] CareerInsertRequest request)
        {
            return Ok(await Mediator.Send(new AddCareerCommand(request)));
        }


        /// <summary>
        /// Get all careers
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllCareerParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllCareerQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Name = filter.Name,
                IsActive = filter.IsActive
            }));
        }

        /// <summary>
        /// Deactivate a career
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            return Ok(await Mediator.Send(new DeactivateCareerCommand(id)));
        }

        /// <summary>
        /// Activate a career
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            return Ok(await Mediator.Send(new EnableCareerCommand(id)));
        }

        /// <summary>
        /// Toggle the status of a career (active/inactive)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleCareerStatus(int id)
        {
            return Ok(await Mediator.Send(new ToggleCareerStatusCommand(id)));
        }

    }
}
