﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using School.Infrastructure.Persistence;

#nullable disable

namespace School.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("School.Domain.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ScienceId")
                        .HasColumnType("integer");

                    b.Property<double>("Score")
                        .HasMaxLength(10)
                        .HasColumnType("double precision");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ScienceId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("School.Domain.Entities.Science", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Sciences");
                });

            modelBuilder.Entity("School.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("School.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("School.Domain.Entities.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("School.Domain.Entities.Student", b =>
                {
                    b.HasBaseType("School.Domain.Entities.User");

                    b.Property<int>("StudentRegNumber")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("School.Domain.Entities.Teacher", b =>
                {
                    b.HasBaseType("School.Domain.Entities.User");

                    b.ToTable("Teacher", (string)null);
                });

            modelBuilder.Entity("School.Domain.Entities.Grade", b =>
                {
                    b.HasOne("School.Domain.Entities.Science", "Science")
                        .WithMany("Grades")
                        .HasForeignKey("ScienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Domain.Entities.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Science");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("School.Domain.Entities.Science", b =>
                {
                    b.HasOne("School.Domain.Entities.Student", "Student")
                        .WithMany("Sciences")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Sciences")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("School.Domain.Entities.Admin", b =>
                {
                    b.HasOne("School.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("School.Domain.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("School.Domain.Entities.Student", b =>
                {
                    b.HasOne("School.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("School.Domain.Entities.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("School.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("School.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("School.Domain.Entities.Teacher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("School.Domain.Entities.Science", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("School.Domain.Entities.Student", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("Sciences");
                });

            modelBuilder.Entity("School.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Sciences");
                });
#pragma warning restore 612, 618
        }
    }
}
