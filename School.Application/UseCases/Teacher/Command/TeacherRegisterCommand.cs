using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Teacher.Command
{
    public class TeacherRegisterCommand : ICommand<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class TeacherRegisterCommandHandler : ICommandHandler<TeacherRegisterCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public TeacherRegisterCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(TeacherRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Teachers.AnyAsync(t => t.UserName == request.UserName, cancellationToken))
            {
                throw new RegisterException();
            }

            var teacher = new Domain.Entities.Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BirthDate = request.BirthDate,
                UserName = request.UserName,
                PasswordHash = _hashService.GetHash(request.Password)
            };

            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}