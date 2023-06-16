using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class DeleteGradeCommand : ICommand<Unit>
	{
		public int Id { get; set; }
	}

    public class DeleteGradeCommandHandler : ICommandHandler<DeleteGradeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGradeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (grade == null)
            {
                throw new GradeNotFoundException();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

