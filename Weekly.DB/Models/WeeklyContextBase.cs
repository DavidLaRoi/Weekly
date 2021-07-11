using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class WeeklyContextBase : DbContext
    {
        public WeeklyContextBase()
        {
        }

        public WeeklyContextBase(DbContextOptions<WeeklyContextBase> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivtyActivity> ActivtyActivities { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Backlog> Backlogs { get; set; }
        public virtual DbSet<BacklogItem> BacklogItems { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Recurrence> Recurrences { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskCte> TaskCtes { get; set; }
        public virtual DbSet<TaskTask> TaskTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.HasIndex(e => e.Name, "IX_Activity")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ActivtyActivity>(entity =>
            {
                entity.ToTable("Activty_Activity");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.HasIndex(e => e.Name, "IX_Appointment")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Backlog>(entity =>
            {
                entity.ToTable("Backlog");

                entity.HasIndex(e => e.Name, "IX_Backlog")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BacklogItem>(entity =>
            {
                entity.ToTable("BacklogItem");

                entity.HasIndex(e => e.Name, "IX_BacklogItem")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.Name, "IX_Group")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.HasIndex(e => e.Name, "IX_Priority")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Recurrence>(entity =>
            {
                entity.ToTable("Recurrence");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.HasIndex(e => e.Name, "IX_Schedule")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.HasIndex(e => e.Name, "IX_Task")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TaskCte>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Task_CTE");

                entity.Property(e => e.ChildId).HasColumnName("ChildID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RootId).HasColumnName("RootID");
            });

            modelBuilder.Entity<TaskTask>(entity =>
            {
                entity.ToTable("Task_Task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ChildTaskId).HasColumnName("ChildTask_ID");

                entity.Property(e => e.ParentTaskId).HasColumnName("ParentTask_ID");

                entity.HasOne(d => d.ChildTask)
                    .WithMany(p => p.TaskTaskChildTasks)
                    .HasForeignKey(d => d.ChildTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Child");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.TaskTaskParentTasks)
                    .HasForeignKey(d => d.ParentTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Task_Parent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
