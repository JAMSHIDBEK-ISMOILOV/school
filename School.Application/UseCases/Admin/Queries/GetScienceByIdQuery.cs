using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;
using School.Application.Exceptions;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetScienceByIdQuery : IQuery<ScienceViewModel>
	{
		public int Id { get; set; }
	}

    public class GetScienceByIdQueryhandler : IQueryHandler<GetScienceByIdQuery, ScienceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetScienceByIdQueryhandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScienceViewModel> Handle(GetScienceByIdQuery request, CancellationToken cancellationToken)
        {
            var science = await _context.Sciences.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (science == null)
            {
                throw new ScienceNotFoundException();
            }

            return new ScienceViewModel
            {
                Name = science.Name
            };
        }
    }
}

