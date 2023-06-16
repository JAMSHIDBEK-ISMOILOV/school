using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class CreateScienceCommand : ICommand<int>
	{
		public string Name { get; set; }
		public int TeacherId { get; set; }
		public int StudentId { get; set; }
	}

    public class CreateScienceCommandHandler : ICommandHandler<CreateScienceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateScienceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateScienceCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Sciences.AnyAsync(x => x.Name == request.Name && x.TeacherId == request.TeacherId && x.StudentId == request.StudentId,
                cancellationToken))
            {
                throw new ScienceExistsException();
            }

            var science = new Domain.Entities.Science
            {
                Name = request.Name,
                TeacherId = request.TeacherId,
                StudentId = request.StudentId
            };

            await _context.Sciences.AddAsync(science);

            await _context.SaveChangesAsync(cancellationToken);

            return science.Id;
        }
    }
}

