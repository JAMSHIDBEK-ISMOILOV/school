using System;
namespace School.Application.Exceptions
{
	public class TeacherExistsException : Exception
	{
        private const string _message = "Teacher Exists!";

        public TeacherExistsException() : base(_message) { }
    }
}

