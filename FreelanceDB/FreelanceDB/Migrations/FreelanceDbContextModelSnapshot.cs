﻿// <auto-generated />
using System;
using FreelanceDB.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelanceDB.Migrations
{
    [DbContext(typeof(FreelancedbContext))]
    partial class FreelanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FreelanceDB.Database.Entities.Response", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("ResponseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("'-infinity'::timestamp with time zone");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("TaskID");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("UserID");

                    b.HasIndex(new[] { "TaskId" }, "IX_Responses_TaskID");

                    b.HasIndex(new[] { "UserId" }, "IX_Responses_UserID");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Resume", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AboutMe")
                        .HasColumnType("text");

                    b.Property<string>("Contacts")
                        .HasColumnType("text");

                    b.Property<string>("Education")
                        .HasColumnType("text");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Skills")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("WorkExp")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Resume_pkey");

                    b.HasIndex("UserId");

                    b.ToTable("Resume", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint")
                        .HasColumnName("AuthorID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<long>("RecipientId")
                        .HasColumnType("bigint")
                        .HasColumnName("RecipientID");

                    b.HasKey("Id")
                        .HasName("Review_pkey");

                    b.HasIndex(new[] { "AuthorId" }, "IX_Review_AuthorID");

                    b.HasIndex(new[] { "RecipientId" }, "IX_Review_RecipientID");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Role1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Role");

                    b.HasKey("Id")
                        .HasName("Role_pkey");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Role1 = "User"
                        },
                        new
                        {
                            Id = 2L,
                            Role1 = "Admin"
                        },
                        new
                        {
                            Id = 3L,
                            Role1 = "Moderator"
                        });
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Status_pkey");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Tag1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Tag");

                    b.HasKey("Id")
                        .HasName("Tag_pkey");

                    b.ToTable("Tag", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Tag1 = "Frontend"
                        },
                        new
                        {
                            Id = 2L,
                            Tag1 = "Backend"
                        },
                        new
                        {
                            Id = 3L,
                            Tag1 = "UI/UX"
                        });
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint")
                        .HasColumnName("AuthorID");

                    b.Property<DateOnly>("Deadline")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<long?>("ExecutorId")
                        .HasColumnType("bigint")
                        .HasColumnName("ExecutorID");

                    b.Property<string>("Head")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("StatusID");

                    b.HasKey("Id")
                        .HasName("Task_pkey");

                    b.HasIndex(new[] { "AuthorId" }, "IX_Task_AuthorID");

                    b.HasIndex(new[] { "ExecutorId" }, "IX_Task_ExecutorID");

                    b.HasIndex(new[] { "StatusId" }, "IX_Task_StatusID");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.TaskTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("TagId")
                        .HasColumnType("bigint");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("TaskTag_pkey");

                    b.HasIndex("TagId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskTag", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AToken")
                        .HasColumnType("text")
                        .HasColumnName("aToken");

                    b.Property<int>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("FreezeBalance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RToken")
                        .HasColumnType("text")
                        .HasColumnName("rToken");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("'-infinity'::timestamp with time zone");

                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id")
                        .HasName("User_pkey");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Response", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.Task", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("Responses_TaskID_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("Responses_UserID_fkey");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Resume", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.User", "User")
                        .WithMany("Resumes")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("Resume_UserId_fkey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Review", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.User", "Author")
                        .WithMany("ReviewAuthors")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("Review_AuthorID_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.User", "Recipient")
                        .WithMany("ReviewRecipients")
                        .HasForeignKey("RecipientId")
                        .IsRequired()
                        .HasConstraintName("Review_RecipientID_fkey");

                    b.Navigation("Author");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Task", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.User", "Author")
                        .WithMany("TaskAuthors")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("Task_AuthorID_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.User", "Executor")
                        .WithMany("TaskExecutors")
                        .HasForeignKey("ExecutorId")
                        .HasConstraintName("Task_ExecutorID_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("Task_StatusID_fkey");

                    b.Navigation("Author");

                    b.Navigation("Executor");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.TaskTag", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.Tag", "Tag")
                        .WithMany("TaskTags")
                        .HasForeignKey("TagId")
                        .IsRequired()
                        .HasConstraintName("TaskTag_TagId_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.Task", "Task")
                        .WithMany("TaskTags")
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("TaskTag_TaskId_fkey");

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.User", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("User_RoleId_fkey");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Tag", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Task", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.User", b =>
                {
                    b.Navigation("Resumes");

                    b.Navigation("ReviewAuthors");

                    b.Navigation("ReviewRecipients");

                    b.Navigation("TaskAuthors");

                    b.Navigation("TaskExecutors");
                });
#pragma warning restore 612, 618
        }
    }
}
