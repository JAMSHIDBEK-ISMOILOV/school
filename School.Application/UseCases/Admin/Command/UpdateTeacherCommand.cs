using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class UpdateTeacherCommand : ICommand<Unit>
	{
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Password { get; set; }
    }

    public class UpdateTeacherCommandHandler : ICommandHandler<UpdateTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public UpdateTeacherCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (teacher == null)
            {
                throw new Exception(nameof(TeacherNotFoundException));
            }

            teacher.FirstName = request.FirstName ?? teacher.FirstName;
            teacher.LastName = request.LastName ?? teacher.LastName;
            teacher.PhoneNumber = request.PhoneNumber ?? teacher.PhoneNumber;
            teacher.Email = request.Email ?? teacher.Email;
            teacher.BirthDate = request.BirthDate ?? teacher.BirthDate;
            teacher.UserName = request.UserName ?? teacher.UserName;
            teacher.PasswordHash = _hashService.GetHash(request.Password);

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

