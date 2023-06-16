using System;
namespace School.Domain.Entities
{
	public class Student : User
	{
        public Student()
        {
            Sciences = new HashSet<Science>();
            Grades = new HashSet<Grade>();
        }

        public int StudentRegNumber { get; set; }

        public ICollection<Science> Sciences { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}

