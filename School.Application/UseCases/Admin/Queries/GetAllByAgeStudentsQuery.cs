using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllByAgeStudentsQuery : IQuery<List<StudentViewModel>>
	{
        public int AgeLimit { get; set; }
    }

    public class GetAllByAgeStudentsQueryHandler : IQueryHandler<GetAllByAgeStudentsQuery, List<StudentViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllByAgeStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> Handle(GetAllByAgeStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.Where(x => (DateTime.Now.Year - x.BirthDate.Year) <= request.AgeLimit)
                                            .Select(x => new StudentViewModel()
                                            {
                                                StudentRegNumber = x.StudentRegNumber,
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

