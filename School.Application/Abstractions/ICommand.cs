using System;
using MediatR;

namespace School.Application.Abstractions
{
	public interface ICommand<out TResponse> : IRequest<TResponse>
    {
	}
}

