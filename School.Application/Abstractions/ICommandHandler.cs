using System;
using MediatR;

namespace School.Application.Abstractions
{
	public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
	}
}

