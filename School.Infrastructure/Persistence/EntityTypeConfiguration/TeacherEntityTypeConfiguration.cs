using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infrastructure.Persistence.EntityTypeConfiguration
{
    public class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher");
        }
    }
}

