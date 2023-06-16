using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class UpdateGradeCommand : ICommand<Unit>
	{
        public int Id { get; set; }
        public double? Score { get; set; }
		public int? ScienceId { get; set; }
		public int? StudentId { get; set; }
	}

    public class UpdateGradeCommandHandler : ICommandHandler<UpdateGradeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public UpdateGradeCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (grade == null)
            {
                throw new Exception(nameof(GradeNotFoundException));
            }

            grade.Score = request.Score ?? grade.Score;
            grade.ScienceId = request.ScienceId ?? grade.ScienceId;
            grade.StudentId = request.StudentId ?? grade.StudentId;

            _context.Grades.Update(grade);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

