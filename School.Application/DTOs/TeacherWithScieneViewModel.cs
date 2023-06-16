using System;
namespace School.Application.DTOs
{
	public class TeacherWithScieneViewModel
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string ScienceName { get; set; }
        public double Score { get; set; }
        public int ScienceStudentRegNumber { get; set; }
    }
}

