using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Domain.Entities;

namespace School.Application.UseCases.Admin.Queries
{
    public class GetAllTeachersByScienceScoreQuery : IQuery<List<TeacherWithScieneViewModel>>
    {
        public double Score { get; set; }
    }

    public class GetAllTeachersByScienceScoreQueryHandler : IQueryHandler<GetAllTeachersByScienceScoreQuery, List<TeacherWithScieneViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTeachersByScienceScoreQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherWithScieneViewModel>> Handle(GetAllTeachersByScienceScoreQuery request, CancellationToken cancellationToken)
        {
            var sciences = await _context.Sciences
                                          .Include(x => x.Grades)
                                          .ThenInclude(x => x.Student)
                                          .Include(x => x.Teacher)
                                          .ToListAsync(cancellationToken);

            var teachers = new List<TeacherWithScieneViewModel>();

            foreach (var science in sciences)
            {
                var grades = science.Grades.ToList();

                foreach (var grade in grades)
                {
                    if (grade.Score >= request.Score)
                    {
                        teachers.Add(new TeacherWithScieneViewModel
                        {
                            FirstName = science.Teacher.FirstName,
                            LastName = science.Teacher.LastName,
                            BirthDate = science.Teacher.BirthDate,
                            Email = science.Teacher.Email,
                            PhoneNumber = science.Teacher.PhoneNumber,
                            ScienceName = science.Name,
                            Score = grade.Score,
                            ScienceStudentRegNumber = grade.Student.StudentRegNumber
                        });
                    }
                }
            }

            return teachers;
        }
    }
}

