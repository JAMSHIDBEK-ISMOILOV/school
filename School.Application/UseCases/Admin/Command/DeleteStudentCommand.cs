using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class DeleteStudentCommand : ICommand<Unit>
	{
		public int Id { get; set; }
	}

    public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (student == null)
            {
                throw new StudentNotFoundException();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

