using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infrastructure.Persistence.EntityTypeConfiguration
{
    public class GradeEntityTypeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Score)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasOne(x => x.Science)
                .WithOne(x => x.Grade)
                .HasForeignKey<Grade>(x => x.ScienceId);

            builder.HasOne(x => x.Student)
                .WithOne(x => x.Grade)
                .HasForeignKey<Grade>(x => x.StudentId);
        }
    }
}

