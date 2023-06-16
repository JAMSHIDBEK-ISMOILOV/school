using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.UseCases.Admin.Command;
using School.Application.UseCases.Admin.Queries;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGradeCommand command)
        {
            var responce = await _mediator.Send(command);
            return Ok(responce);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateGradeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteGradeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _mediator.Send(new GetAllGradesQuery());
            if (grades is null)
            {
                return Ok("");
            }
            return Ok(grades);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var grade = await _mediator.Send(new GetGradeByIdQuery { Id = id });
            if (grade is null)
            {
                return Ok("");
            }
            return Ok(grade);
        }
    }
}

