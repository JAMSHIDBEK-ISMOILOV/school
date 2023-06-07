using System;
namespace School.Domain.Entities
{
	public class Teacher : User
	{
        public Teacher()
        {
            Sciences = new HashSet<Science>();
        }

        public ICollection<Science> Sciences { get; set; }
    }
}

