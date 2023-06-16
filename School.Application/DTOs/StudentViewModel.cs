using System;
namespace School.Application.DTOs
{
	public class StudentViewModel
	{
        public int StudentRegNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

