using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class UpdateStudentCommand : ICommand<Unit>
	{
        public int Id { get; set; }
        public int? StudentRegNumber { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Password { get; set; }
    }

    public class UpdateStudentCommandHandler : ICommandHandler<UpdateStudentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public UpdateStudentCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (student == null)
            {
                throw new Exception(nameof(StudentNotFoundException));
            }

            student.StudentRegNumber = request.StudentRegNumber ?? student.StudentRegNumber;
            student.FirstName = request.FirstName ?? student.FirstName;
            student.LastName = request.LastName ?? student.LastName;
            student.PhoneNumber = request.PhoneNumber ?? student.PhoneNumber;
            student.Email = request.Email ?? student.Email;
            student.BirthDate = request.BirthDate ?? student.BirthDate;
            student.UserName = request.UserName ?? student.UserName;
            student.PasswordHash = _hashService.GetHash(request.Password);

            _context.Students.Update(student);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

