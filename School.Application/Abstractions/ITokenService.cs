using System;
using System.Security.Claims;

namespace School.Application.Abstractions
{
	public interface ITokenService
	{
        string GetAccessToken(Claim[] claims);
    }
}

