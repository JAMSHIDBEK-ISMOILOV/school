using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.UseCases.Admin.Command;
using School.Application.UseCases.Admin.Queries;
using School.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace School.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator, IApplicationDbContext context)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminCommand command)
        {
            var responce = await _mediator.Send(command);
            return Ok(responce);
        }

        [Authorize]
        [HttpGet("Students/ageLimit/{limit}")]
        public async Task<IActionResult> GetAllByAge([FromRoute] int limit)
        {
            var students = await _mediator.Send(new GetAllByAgeStudentsQuery { AgeLimit = limit });
            if (students.Count == 0)
            {
                return Ok("");
            }
            return Ok(students);
        }

        [Authorize]
        [HttpGet("Students/monthLimit/{from}/{to}")]
        public async Task<IActionResult> GetAllByBirthdate([FromRoute] DateTimeOffset from, DateTimeOffset to)
        {
            var students = await _mediator.Send(new GetAllStudentsBirthdayQuery { From = from, To = to});
            if (students.Count == 0)
            {
                return Ok("");
            }
            return Ok(students);
        }

        [Authorize]
        [HttpGet("Teachers/ageLimit/{limit}")]
        public async Task<IActionResult> GetAllByAgeTeachers([FromRoute] int limit)
        {
            var teachers = await _mediator.Send(new GetAllByAgeTeachersQuery { AgeLimit = limit });
            if (teachers.Count == 0)
            {
                return Ok("");
            }
            return Ok(teachers);
        }

        [Authorize]
        [HttpGet("Users/phoneCode/{code}")]
        public async Task<IActionResult> GetAllByPhoneNumberUsers([FromRoute] string code)
        {
            var users = await _mediator.Send(new GetAllUsersQuery { Code = code});
            if (users.Count == 0)
            {
                return Ok("");
            }
            return Ok(users);
        }

        [Authorize]
        [HttpGet("Students/phrase/{phrase}")]
        public async Task<IActionResult> GetAllByPhraseStudents([FromRoute] string phrase)
        {
            var students = await _mediator.Send(new GetAllStudentsPhraseQuery { Phrase = phrase});
            if (students.Count == 0)
            {
                return Ok("");
            }
            return Ok(students);
        }

        [Authorize]
        [HttpGet("Science/studentNumber/{number}")]
        public async Task<IActionResult> GetByMaxScoreScience([FromRoute] int number)
        {
            var science = await _mediator.Send(new GetByScienceQuery { Number = number});
            if (science is null)
            {
                return Ok("");
            }
            return Ok(science);
        }

        [Authorize]
        [HttpGet("Science/studentNumber/{teacher_id}/{score}")]
        public async Task<IActionResult> GetStudentsOfSelectedTeacher([FromRoute] int teacher_id, int score)
        {
            var students = await _mediator.Send(new GetStudentsOfSelectedTeacherQuery { Score = score, TeacherId = teacher_id });
            if (students.Count == 0)
            {
                return Ok("");
            }
            return Ok(students);
        }

        [Authorize]
        [HttpGet("Science/Score/{score}")]
        public async Task<IActionResult> GetAllTeachersByScienceScore([FromRoute] double score)
        {
            var teachers = await _mediator.Send(new GetAllTeachersByScienceScoreQuery { Score = score });
            if (teachers.Count == 0)
            {
                return Ok("");
            }
            return Ok(teachers);
        }

        [Authorize]
        [HttpGet("Science/studentAvarageScore/{student_id}")]
        public async Task<IActionResult> GetWithByAvarageScoreScience([FromRoute] int student_id)
        {
            var science = await _mediator.Send(new GetWithByAvarageScoreScienceQuery { StudentId = student_id });
            if (science is null)
            {
                return Ok("");
            }
            return Ok(science);
        }
    }
}

