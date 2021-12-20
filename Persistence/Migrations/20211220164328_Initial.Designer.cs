﻿// <auto-generated />
using System;
using JustAnotherToDo.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JustAnotherToDo.Persistence.Migrations
{
    [DbContext(typeof(JustAnotherToDoDbContext))]
    [Migration("20211220164328_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FK_Todos_UserProfile_ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FK_Todos_UserProfile_ProfileId");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.Category", b =>
                {
                    b.HasOne("JustAnotherToDo.Domain.Entities.UserProfile", "Profile")
                        .WithMany("Categories")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Categories_UserProfile_ProfileId");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.ToDo", b =>
                {
                    b.HasOne("JustAnotherToDo.Domain.Entities.Category", "Category")
                        .WithMany("ToDos")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Categories_Todo_CategoryId");

                    b.HasOne("JustAnotherToDo.Domain.Entities.UserProfile", "Profile")
                        .WithMany("ToDos")
                        .HasForeignKey("FK_Todos_UserProfile_ProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.Category", b =>
                {
                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("JustAnotherToDo.Domain.Entities.UserProfile", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("ToDos");
                });
#pragma warning restore 612, 618
        }
    }
}
