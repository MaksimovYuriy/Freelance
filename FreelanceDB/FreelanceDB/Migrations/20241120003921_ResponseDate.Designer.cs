﻿// <auto-generated />
using System;
using FreelanceDB.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelanceDB.Migrations
{
    [DbContext(typeof(FreelanceDbContext))]
    [Migration("20241120003921_ResponseDate")]
    partial class ResponseDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("TaskID");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("UserID");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

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

                    b.Property<string>("WorkExp")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Resume_pkey");

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

                    b.HasIndex("AuthorId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Review", (string)null);
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

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Task_pkey");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("StatusId");

                    b.ToTable("Task", (string)null);
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
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("User_pkey");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.UserResume", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ResumeId")
                        .HasColumnType("bigint")
                        .HasColumnName("ResumeID");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("UserID");

                    b.HasIndex("ResumeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserResume", (string)null);
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

            modelBuilder.Entity("FreelanceDB.Database.Entities.UserResume", b =>
                {
                    b.HasOne("FreelanceDB.Database.Entities.Resume", "Resume")
                        .WithMany()
                        .HasForeignKey("ResumeId")
                        .IsRequired()
                        .HasConstraintName("UserResume_ResumeID_fkey");

                    b.HasOne("FreelanceDB.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("UserResume_UserID_fkey");

                    b.Navigation("Resume");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("FreelanceDB.Database.Entities.User", b =>
                {
                    b.Navigation("ReviewAuthors");

                    b.Navigation("ReviewRecipients");

                    b.Navigation("TaskAuthors");

                    b.Navigation("TaskExecutors");
                });
#pragma warning restore 612, 618
        }
    }
}
