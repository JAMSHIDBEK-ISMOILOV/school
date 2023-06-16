using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class DeleteScienceCommand : ICommand<Unit>
    {
		public int Id { get; set; }
	}

    public class DeleteScienceCommandHandler : ICommandHandler<DeleteScienceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteScienceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteScienceCommand request, CancellationToken cancellationToken)
        {
            var science = await _context.Grades.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (science == null)
            {
                throw new ScienceNotFoundException();
            }

            _context.Grades.Remove(science);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

