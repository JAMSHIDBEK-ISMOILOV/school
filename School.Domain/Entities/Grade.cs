using System;
namespace School.Domain.Entities
{
	public class Grade
	{
        public int Id { get; set; }
        public double Score { get; set; }
        public int ScienceId { get; set; }
        public int StudentId { get; set; }

        public Science Science { get; set; }
        public Student Student { get; set; }
    }
}

