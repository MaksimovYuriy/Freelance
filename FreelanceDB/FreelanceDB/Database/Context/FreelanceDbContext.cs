using System;
using System.Collections.Generic;
using FreelanceDB.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelanceDB.Database.Context;

public partial class FreelanceDbContext : DbContext
{
    public FreelanceDbContext()
    {
    }

    public FreelanceDbContext(DbContextOptions<FreelanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Entities.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserResume> UserResumes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Freelancedb;Username=developer;Password=developer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Task).WithMany()
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Responses_TaskID_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Responses_UserID_fkey");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Resume_pkey");

            entity.ToTable("Resume");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Review_pkey");

            entity.ToTable("Review");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.RecipientId).HasColumnName("RecipientID");

            entity.HasOne(d => d.Author).WithMany(p => p.ReviewAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_AuthorID_fkey");

            entity.HasOne(d => d.Recipient).WithMany(p => p.ReviewRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_RecipientID_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Status_pkey");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Entities.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.ExecutorId).HasColumnName("ExecutorID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Author).WithMany(p => p.TaskAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_AuthorID_fkey");

            entity.HasOne(d => d.Executor).WithMany(p => p.TaskExecutors)
                .HasForeignKey(d => d.ExecutorId)
                .HasConstraintName("Task_ExecutorID_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_StatusID_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AToken).HasColumnName("aToken");
            entity.Property(e => e.Balance).HasDefaultValue(0);
            entity.Property(e => e.FreezeBalance).HasDefaultValue(0);
            entity.Property(e => e.RToken).HasColumnName("rToken");
        });

        modelBuilder.Entity<UserResume>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserResume");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ResumeId).HasColumnName("ResumeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Resume).WithMany()
                .HasForeignKey(d => d.ResumeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserResume_ResumeID_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserResume_UserID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
