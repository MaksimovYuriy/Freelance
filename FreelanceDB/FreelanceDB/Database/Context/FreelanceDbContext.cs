
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FreelanceDB.Database.Entities;

namespace FreelanceDB.Database.Context;

public partial class FreelancedbContext : DbContext
{
    public FreelancedbContext()
    {
    }

    public FreelancedbContext(DbContextOptions<FreelancedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Entities.Task> Tasks { get; set; }

    public virtual DbSet<TaskTag> TaskTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Freelancedb;Username=developer;Password=developer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.TaskId, "IX_Responses_TaskID");

            entity.HasIndex(e => e.UserId, "IX_Responses_UserID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ResponseDate).HasDefaultValueSql("'-infinity'::timestamp with time zone");
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

            entity.HasOne(d => d.User).WithMany(p => p.Resumes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Resume_UserId_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Review_pkey");

            entity.ToTable("Review");

            entity.HasIndex(e => e.AuthorId, "IX_Review_AuthorID");

            entity.HasIndex(e => e.RecipientId, "IX_Review_RecipientID");

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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Role1).HasColumnName("Role");
            entity.HasData(
                new Role[]
                {
                    new Role{Id=1, Role1="User"},
                    new Role{Id=2,Role1="Admin"},
                    new Role{Id=3,Role1="Moderator"}
                });
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Status_pkey");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pkey");

            entity.ToTable("Tag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tag1).HasColumnName("Tag");
            entity.HasData(
                new Tag[]
                {
                    new Tag{Id=1, Tag1="Frontend"},
                    new Tag{Id=2, Tag1="Backend"},
                    new Tag{Id=3, Tag1="UI/UX"}
                }
                );
        });

        modelBuilder.Entity<Entities.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pkey");

            entity.ToTable("Task");

            entity.HasIndex(e => e.AuthorId, "IX_Task_AuthorID");

            entity.HasIndex(e => e.ExecutorId, "IX_Task_ExecutorID");

            entity.HasIndex(e => e.StatusId, "IX_Task_StatusID");

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

        modelBuilder.Entity<TaskTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TaskTag_pkey");

            entity.ToTable("TaskTag");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Tag).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskTag_TagId_fkey");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskTag_TaskId_fkey");
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
            entity.Property(e => e.RefreshTokenExpiryTime).HasDefaultValueSql("'-infinity'::timestamp with time zone");
            entity.Property(e => e.RoleId).HasDefaultValue(0L);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_RoleId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
