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
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var responce = await _mediator.Send(command);
            return Ok(responce);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteStudentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _mediator.Send(new GetAllStudentsQuery());
            if (teachers is null)
            {
                return Ok("");
            }
            return Ok(teachers);
        }

        [Authorize(Policy = "AdminActions")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var teacher = await _mediator.Send(new GetStudentByIdQuery { Id = id });
            if (teacher is null)
            {
                return Ok("");
            }
            return Ok(teacher);
        }
    }
}

