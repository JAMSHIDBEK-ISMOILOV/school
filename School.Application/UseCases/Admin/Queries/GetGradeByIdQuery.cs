using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetGradeByIdQuery : IQuery<GradeViewModel>
	{
		public int Id { get; set; }
	}

    public class GetGradeByIdQueryHandler : IQueryHandler<GetGradeByIdQuery, GradeViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetGradeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GradeViewModel> Handle(GetGradeByIdQuery request, CancellationToken cancellationToken)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (grade == null)
            {
                throw new GradeNotFoundException();
            }

            return new GradeViewModel
            {
                Score = grade.Score,
                ScienceId = grade.ScienceId,
                StudentId = grade.StudentId
            };
        }
    }
}

