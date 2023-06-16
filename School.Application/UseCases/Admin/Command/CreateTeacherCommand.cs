using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class CreateTeacherCommand : ICommand<Unit>
	{
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }

    public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;
        public CreateTeacherCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (teacher != null)
            {
                throw new Exception(nameof(TeacherExistsException));
            }

            await _context.Teachers.AddAsync(new Domain.Entities.Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BirthDate = request.BirthDate,
                UserName = request.UserName,
                PasswordHash = _hashService.GetHash(request.Password)
            },cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

