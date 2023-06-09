using System;
namespace School.Application.Abstractions
{
	public interface IHashService
	{
        string GetHash(string value);
    }
}

