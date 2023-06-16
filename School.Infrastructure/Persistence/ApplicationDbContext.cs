using System;
using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions;
using School.Domain.Entities;

namespace School.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Science> Sciences { get; set; }
        public DbSet<Student> Students {get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}

