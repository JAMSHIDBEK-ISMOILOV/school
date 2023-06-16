using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllStudentsBirthdayQuery : IQuery<List<StudentViewModel>>
	{
		public DateTimeOffset From { get; set; }
		public DateTimeOffset To { get; set; }
	}

    public class GetAllStudentsBirthdayQueryHandler : IQueryHandler<GetAllStudentsBirthdayQuery, List<StudentViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsBirthdayQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> Handle(GetAllStudentsBirthdayQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students.Where(x => x.BirthDate.Month >= request.From.Month
                                                     && x.BirthDate.Day >= request.From.Day
                                                     && x.BirthDate.Month <= request.To.Month
                                                     && x.BirthDate.Day <= request.To.Day).Select(x => new StudentViewModel
                                                     {
                                                         StudentRegNumber = x.StudentRegNumber,
                                                         FirstName = x.FirstName,
                                                         LastName = x.LastName,
                                                         PhoneNumber = x.PhoneNumber,
                                                         Email = x.Email,
                                                         BirthDate = x.BirthDate
                                                     }).ToListAsync(cancellationToken);
            return students;
        }
    }
}

