using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllUsersQuery : IQuery<List<UserViewModel>>
	{
        public string Code { get; set; }
    }

    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Where(x => x.PhoneNumber.Substring(4, 2) == request.Code)
                                            .Select(x => new UserViewModel
                                            {
                                                FirstName = x.FirstName,
                                                LastName = x.LastName,
                                                PhoneNumber = x.PhoneNumber,
                                                Email = x.Email,
                                                BirthDate = x.BirthDate
                                            }).ToListAsync(cancellationToken);
            return users;       
        }
    }
}

