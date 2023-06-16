using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Command
{
	public class CreateStudentCommand : ICommand<Unit>
	{
        public int StudentRegNumber { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }

    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public CreateStudentCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (student != null)
            {
                throw new Exception(nameof(StudentExistsException));
            }

            await _context.Students.AddAsync(new Domain.Entities.Student
            {
                StudentRegNumber = request.StudentRegNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BirthDate = request.BirthDate,
                UserName = request.UserName,
                PasswordHash = _hashService.GetHash(request.Password)
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

