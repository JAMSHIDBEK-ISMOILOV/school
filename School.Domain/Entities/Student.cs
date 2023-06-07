using System;
namespace School.Domain.Entities
{
	public class Student : User
	{
        public Student()
        {
            Sciences = new HashSet<Science>();
        }

        public int StudentRegNumber { get; set; }

        public Grade Grade { get; set; }
        public ICollection<Science> Sciences { get; set; }
    }
}

