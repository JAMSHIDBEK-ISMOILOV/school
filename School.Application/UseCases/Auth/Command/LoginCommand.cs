using System;
using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.Exceptions;
using School.Domain.Entities;
using School.Domain.Exceptions;

namespace School.Application.UseCases.Auth.Command
{
    public class LoginCommand : IRequest<string>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IHashService _hashService;
            private readonly ITokenService _tokenService;

            public LoginCommandHandler(IApplicationDbContext context, IHashService hashService, ITokenService tokenService)
            {
                _context = context;
                _hashService = hashService;
                _tokenService = tokenService;
            }

            public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

                if (user == null)
                {
                    throw new LoginException(new EntityNotFoundException(nameof(User)));
                }

                if (user.PasswordHash != _hashService.GetHash(request.Password))
                {
                    throw new LoginException();
                }

                var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.UserName)
            };

                if (await _context.Teachers.AnyAsync(x => x.Id == user.Id, cancellationToken))
                {
                    claims.Add(new Claim(ClaimTypes.Role, nameof(Domain.Entities.Admin)));
                }

                if (await _context.Students.AnyAsync(x => x.Id == user.Id, cancellationToken))
                {
                    claims.Add(new Claim(ClaimTypes.Role, nameof(Domain.Entities.Admin)));
                }

                return _tokenService.GetAccessToken(claims.ToArray());
            }
        }
    }
}

