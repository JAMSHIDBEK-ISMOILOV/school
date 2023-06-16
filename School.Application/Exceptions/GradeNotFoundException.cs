using System;
namespace School.Application.Exceptions
{
	public class GradeNotFoundException : Exception
	{
		private const string _message = "Grade";

		public GradeNotFoundException() : base(_message){}
	}
}

