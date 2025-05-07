using Application.Feautures._subject.Commands.AddSubjectCommands;
using Application.Feautures._subject.Commands.DeactivateSubjectCommands;
using Application.Feautures._subject.Commands.EnableSubjectCommands;
using Application.Feautures._subject.Commands.ToggleSubjectCommands;
using Application.Feautures._subject.Queries.GetAllSubjectQueries;
using Microsoft.AspNetCore.Mvc;
using Models._subject.Request;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class SubjectController : BaseApiController
    {
        /// <summary>
        /// Add a new subject
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectInsertRequest request)
        {
            return Ok(await Mediator.Send(new AddSubjectCommand(request)));
        }

        /// <summary>
        /// Get all subjects
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] GetAllSubjectParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllSubjectQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Name = filter.Name,
                IsActive = filter.IsActive
            }));
        }

        /// <summary>
        /// Deactivate a subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            return Ok(await Mediator.Send(new DeactivateSubjectCommand(id)));
        }

        /// <summary>
        /// Activate a subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            return Ok(await Mediator.Send(new EnableSubjectCommand(id)));
        }

        /// <summary>
        /// Toggle the status of a subject (active/inactive)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            return Ok(await Mediator.Send(new ToggleSubjectCommand(id)));
        }

    }
}
