using System;
namespace School.Application.Exceptions
{
	public class TeacherNotFoundException : Exception
	{
        private const string _message = "Teacher";

        public TeacherNotFoundException() : base(_message) { }
    }
}

