using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetStudentByIdQuery : IQuery<StudentViewModel>
	{
		public int Id { get; set; }
	}

    public class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery, StudentViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (student == null)
            {
                throw new StudentNotFoundException();
            }

            return new StudentViewModel
            {
                StudentRegNumber = student.StudentRegNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                BirthDate = student.BirthDate
            };
        }
    }
}

