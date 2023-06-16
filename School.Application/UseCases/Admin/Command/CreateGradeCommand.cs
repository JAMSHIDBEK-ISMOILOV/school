using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class CreateGradeCommand : ICommand<int>
	{
		public double Score { get; set; }
		public int ScienceId { get; set; }
		public int StudentId { get; set; }
	}

    public class CreateGradeCommandHandler : ICommandHandler<CreateGradeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateGradeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Grades.AnyAsync(x => x.Score == request.Score && x.ScienceId == request.ScienceId && x.StudentId == request.StudentId,
                cancellationToken))
            {
                throw new ScienceExistsException();
            }

            var grade = new Domain.Entities.Grade
            {
                Score = request.Score,
                ScienceId = request.ScienceId,
                StudentId = request.StudentId
            };

            await _context.Grades.AddAsync(grade);

            await _context.SaveChangesAsync(cancellationToken);

            return grade.Id;
        }
    }
}

