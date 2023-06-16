using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetStudentsOfSelectedTeacherQuery : IQuery<List<StudentWithScieneViewModel>>
    {
        public int TeacherId { get; set; }
        public double Score { get; set; }
    }

    public class GetStudentsOfSelectedTeacherQueryHandler : IQueryHandler<GetStudentsOfSelectedTeacherQuery, List<StudentWithScieneViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentsOfSelectedTeacherQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentWithScieneViewModel>> Handle(GetStudentsOfSelectedTeacherQuery request, CancellationToken cancellationToken)
        {
            var sciences = await _context.Sciences
                                         .Include(x => x.Grades)
                                         .ThenInclude(x => x.Student)
                                         .ToListAsync(cancellationToken);

            var students = new List<StudentWithScieneViewModel>();

            foreach (var science in sciences)
            {
                if (science.TeacherId == request.TeacherId)
                {
                    var grades = science.Grades.ToList();

                    foreach (var grade in grades)
                    {
                        if (grade.Score >= request.Score)
                        {
                            students.Add(new StudentWithScieneViewModel
                            {
                                FirstName = grade.Student.FirstName,
                                LastName = grade.Student.LastName,
                                BirthDate = grade.Student.BirthDate,
                                Email = grade.Student.Email,
                                StudentRegNumber = grade.Student.StudentRegNumber,
                                PhoneNumber = grade.Student.PhoneNumber,
                                ScienceName = science.Name,
                                Score = grade.Score
                            });
                        }
                    }
                }
            }

            return students;
        }
    }
}

