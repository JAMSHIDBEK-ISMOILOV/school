using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class CreateAdminCommand : ICommand<Unit>
	{
		public int Id { get; set; }
	}

    public class CreateAdminCommandHandler : ICommandHandler<CreateAdminCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _service;

        public CreateAdminCommandHandler(IApplicationDbContext context, IHashService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (admin != null)
            {
                throw new Exception(nameof(AdminExistsException));
            }

            await _context.Admins.AddAsync(new Domain.Entities.Admin
            {
                Id = request.Id
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

