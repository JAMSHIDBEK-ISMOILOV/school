using System;
namespace School.Application.Exceptions
{
	public class AdminExistsException : Exception
	{
		private const string _message = "Admin exists!";

		public AdminExistsException() : base(_message) { }
	}
}

