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
    public class ScienceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScienceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateScienceCommand command)
        {
            var responce = await _mediator.Send(command);
            return Ok(responce);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateScienceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteScienceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var science = await _mediator.Send(new GetAllSciencesQuery());
            if (science is null)
            {
                return Ok("");
            }
            return Ok(science);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var science = await _mediator.Send(new GetScienceByIdQuery { Id = id });
            if (science is null)
            {
                return Ok("");
            }
            return Ok(science);
        }
    }
}

