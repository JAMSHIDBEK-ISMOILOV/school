using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Student.Command
{
    public class StudentRegisterCommand : ICommand<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int StudentRegNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public class StudentRegisterCommandHandler : ICommandHandler<StudentRegisterCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IHashService _hashService;

            public StudentRegisterCommandHandler(IApplicationDbContext context, IHashService hashService)
            {
                _context = context;
                _hashService = hashService;
            }

            public async Task<Unit> Handle(StudentRegisterCommand request, CancellationToken cancellationToken)
            {
                if(await _context.Students.AnyAsync(s => s.UserName == request.UserName, cancellationToken))
                {
                    throw new RegisterException();
                }

                var student = new Domain.Entities.Student
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    StudentRegNumber = request.StudentRegNumber,
                    UserName = request.UserName,
                    PasswordHash = _hashService.GetHash(request.Password)
                };

                await _context.Students.AddAsync(student, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

