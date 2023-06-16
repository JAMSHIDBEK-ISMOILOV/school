using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetTeacherByIdQuery : IQuery<TeacherViewModel>
	{
        public int Id { get; set; }
    }

    public class GetTeacherByIdQueryHandler : IQueryHandler<GetTeacherByIdQuery, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetTeacherByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherViewModel> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            return new TeacherViewModel
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                Email = teacher.Email,
                BirthDate = teacher.BirthDate
            };
        }
    }
}

