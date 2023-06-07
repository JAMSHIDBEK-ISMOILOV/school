using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infrastructure.Persistence.EntityTypeConfiguration
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");
        }
    }
}

