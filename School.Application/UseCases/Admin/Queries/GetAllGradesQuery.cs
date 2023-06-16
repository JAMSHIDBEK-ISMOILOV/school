using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllGradesQuery : IQuery<List<GradeViewModel>>
	{
	}

    public class GetAllGradesQueryHandler : IQueryHandler<GetAllGradesQuery, List<GradeViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllGradesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GradeViewModel>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Grades
                .Select(x => new GradeViewModel
                {
                    Score = x.Score,
                    ScienceId = x.ScienceId,
                    StudentId = x.StudentId
                }).ToListAsync(cancellationToken);
        }
    }
}

