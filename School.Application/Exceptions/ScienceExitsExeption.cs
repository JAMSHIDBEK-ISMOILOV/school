using System;
namespace School.Application.Exceptions
{
	public class ScienceExistsException : Exception
	{
        private const string _message = "Science exists!";

        public ScienceExistsException() : base(_message) { }
    }
}

