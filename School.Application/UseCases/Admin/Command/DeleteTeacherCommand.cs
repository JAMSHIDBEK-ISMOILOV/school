using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class DeleteTeacherCommand : ICommand<Unit>
	{
		public int Id { get; set; }
	}

    public class DeleteTeacherCommandHandler : ICommandHandler<DeleteTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

