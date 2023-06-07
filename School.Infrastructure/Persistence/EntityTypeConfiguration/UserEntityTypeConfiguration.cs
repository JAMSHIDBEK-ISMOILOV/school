using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infrastructure.Persistence.EntityTypeConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.UserName)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(i => i.UserName)
                .IsUnique();
        }
    }
}

