using System;
using MediatR;

namespace School.Application.Abstractions
{
	public interface IQuery<out TResponse> : IRequest<TResponse>
    {
	}
}

