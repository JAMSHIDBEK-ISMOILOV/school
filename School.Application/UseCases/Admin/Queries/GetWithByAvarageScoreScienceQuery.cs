using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Domain.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetWithByAvarageScoreScienceQuery : IQuery<ScienceViewModel>
    {
		public int StudentId { get; set; }
	}

    public class GetWithByAvarageScoreScienceQueryHandler : IQueryHandler<GetWithByAvarageScoreScienceQuery, ScienceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetWithByAvarageScoreScienceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScienceViewModel> Handle(GetWithByAvarageScoreScienceQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(x => x.Grades)
                .Include(x => x.Sciences)
                .FirstOrDefaultAsync(x => x.Id == request.StudentId, cancellationToken);

            if (student == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Student));
            }

            var maxGrade = student.Grades.First();

            foreach (var item in student.Grades)
            {
                if (item.Score > maxGrade.Score)
                {
                    maxGrade = item;
                }
            }

            var science = new ScienceViewModel
            {
                Name = maxGrade.Science.Name
            };

            return science;
        }
    }
}

