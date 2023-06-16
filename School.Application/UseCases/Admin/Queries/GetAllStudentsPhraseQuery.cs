using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllStudentsPhraseQuery : IQuery<List<StudentViewModel>>
	{
        public string Phrase { get; set; }
    }

    public class GetAllStudentsPhraseQueryHandler : IQueryHandler<GetAllStudentsPhraseQuery, List<StudentViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsPhraseQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> Handle(GetAllStudentsPhraseQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students.Where(x => x.FirstName.Contains(request.Phrase) || x.LastName.Contains(request.Phrase))
                                                    .Select(x => new StudentViewModel
                                                    {
                                                        StudentRegNumber = x.StudentRegNumber,
                                                        FirstName = x.FirstName,
                                                        LastName = x.LastName,
                                                        PhoneNumber = x.PhoneNumber,
                                                        Email = x.Email,
                                                        BirthDate = x.BirthDate
                                                    }).ToListAsync(cancellationToken);
            return students;
        }
    }
}

