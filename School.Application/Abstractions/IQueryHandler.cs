using System;
using MediatR;

namespace School.Application.Abstractions
{
	public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
		
	}
}

