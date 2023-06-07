using System;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Application.Abstractions
{
	public interface IApplicationDbContext
	{
        DbSet<Admin> Admins { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Science> Sciences { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

