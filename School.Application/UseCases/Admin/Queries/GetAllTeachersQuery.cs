using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllTeachersQuery : IQuery<List<TeacherViewModel>>
	{
		
	}

    public class GetAllTeachersQueryHandler : IQueryHandler<GetAllTeachersQuery, List<TeacherViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTeachersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherViewModel>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teachers
                .Select(x => new TeacherViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    BirthDate = x.BirthDate
                }).ToListAsync(cancellationToken);
        }
    }
}

