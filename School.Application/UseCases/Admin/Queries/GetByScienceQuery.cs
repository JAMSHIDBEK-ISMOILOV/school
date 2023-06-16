using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Application.Exceptions;
using School.Domain.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
    public class GetByScienceQuery : IQuery<ScienceViewModel>
    {
        public int Number { get; set; }
    }

    public class GetByScienceQueryHandler : IQueryHandler<GetByScienceQuery, ScienceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetByScienceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScienceViewModel> Handle(GetByScienceQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(x => x.Grades)
                .Include(x => x.Sciences)
                .FirstOrDefaultAsync(x => x.StudentRegNumber == request.Number, cancellationToken);

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