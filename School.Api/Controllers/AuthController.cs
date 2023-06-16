using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.UseCases.Auth.Command;
using School.Application.UseCases.Student.Command;
using School.Application.UseCases.Teacher.Command;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Teacher/Register")]
        public async Task<IActionResult> RegisterAsTeacher(TeacherRegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("Student/Register")]
        public async Task<IActionResult> RegisterAsStudent(StudentRegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}

