using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class UpdateScienceCommand : ICommand<Unit>
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? TeacherId { get; set; }
		public int? StudentId { get; set; }
	}

    public class UpdateScienceCommandHandler : ICommandHandler<UpdateScienceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public UpdateScienceCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(UpdateScienceCommand request, CancellationToken cancellationToken)
        {
            var science = await _context.Sciences.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (science == null)
            {
                throw new Exception(nameof(ScienceNotFoundException));
            }

            science.Name = request.Name ?? science.Name;
            science.TeacherId = request.TeacherId ?? science.TeacherId;
            science.StudentId = request.StudentId ?? science.StudentId;

            _context.Sciences.Update(science);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

