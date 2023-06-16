using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Application.DTOs;

namespace School.Application.UseCases.Admin.Queries
{
	public class GetAllSciencesQuery : IQuery<List<ScienceViewModel>>
	{ 
		
	}

    public class GetAllSciencesQueryHandler : IQueryHandler<GetAllSciencesQuery, List<ScienceViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllSciencesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScienceViewModel>> Handle(GetAllSciencesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Sciences
                .Select(x => new ScienceViewModel
                {
                    Name = x.Name
                }).ToListAsync(cancellationToken);
        }
    }
}

