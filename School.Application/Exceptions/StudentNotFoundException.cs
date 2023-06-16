using System;
namespace School.Application.Exceptions
{
	public class StudentNotFoundException : Exception
	{
        private const string _message = "Student not found!";

        public StudentNotFoundException()
            : base(_message) { }

        public StudentNotFoundException(Exception innerException)
            : base(_message, innerException) { }
    }
}

