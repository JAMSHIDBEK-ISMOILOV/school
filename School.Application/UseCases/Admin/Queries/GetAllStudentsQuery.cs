using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllStudentsQuery : IQuery<List<StudentViewModel>>
	{
		
	}

    public class GetAllStudentsQueryHandler : IQueryHandler<GetAllStudentsQuery, List<StudentViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students
                .Select(x => new StudentViewModel()
                {
                    StudentRegNumber = x.StudentRegNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    BirthDate = x.BirthDate
                }).ToListAsync(cancellationToken);
        }
    }
}

