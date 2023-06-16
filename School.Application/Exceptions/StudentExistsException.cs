using System;
namespace School.Application.Exceptions
{
	public class StudentExistsException : Exception
	{
        private const string _message = "Student Exists!";

        public StudentExistsException() : base(_message) { }
    }
}

