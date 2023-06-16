using System;
namespace School.Application.Exceptions
{
	public class ScienceNotFoundException : Exception
	{
		private const string _message = "Science";

		public ScienceNotFoundException() : base(_message){ }
	}
}

