using System;
namespace School.Domain.Entities
{
	public class Science
	{
        public Science()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }

        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}

