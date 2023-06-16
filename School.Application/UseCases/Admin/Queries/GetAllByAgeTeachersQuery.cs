using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllByAgeTeachersQuery : IQuery<List<TeacherViewModel>>
	{
		public int AgeLimit { get; set; }
	}

    public class GetAllByAgeTeachersQueryHandler : IQueryHandler<GetAllByAgeTeachersQuery, List<TeacherViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllByAgeTeachersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherViewModel>> Handle(GetAllByAgeTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Teachers.Where(x => (DateTime.Now.Year - x.BirthDate.Year) >= request.AgeLimit)
                                            .Select(x => new TeacherViewModel()
                                            {
                                                FirstName = x.FirstName,
                                                LastName = x.LastName,
                                                PhoneNumber = x.PhoneNumber,
                                                Email = x.Email,
                                                BirthDate = x.BirthDate
                                            }
                                            ).ToListAsync(cancellationToken);
        }
    }
}

