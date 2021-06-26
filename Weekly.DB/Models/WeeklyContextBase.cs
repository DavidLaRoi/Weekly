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

        public virtual DbSet<Backlog> Backlogs { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskCte> TaskCtes { get; set; }
        public virtual DbSet<TaskHasTask> TaskHasTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-V2N2JBV\\SQLEXPRESS;Database=Weekly;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Backlog>(entity =>
            {
                entity.ToTable("Backlog");

                entity.HasIndex(e => e.Name, "IX_Backlog")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.HasIndex(e => e.Name, "CK_Schedule_Name")
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

            modelBuilder.Entity<TaskHasTask>(entity =>
            {
                entity.HasKey(e => new { e.SubTaskId, e.ParentTaskId })
                    .HasName("PK_Task_Has_Task_1");

                entity.ToTable("Task_Has_Task");

                entity.Property(e => e.SubTaskId).HasColumnName("SubTask_ID");

                entity.Property(e => e.ParentTaskId).HasColumnName("ParentTask_ID");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.TaskHasTaskParentTasks)
                    .HasForeignKey(d => d.ParentTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Task_Parent");

                entity.HasOne(d => d.SubTask)
                    .WithMany(p => p.TaskHasTaskSubTasks)
                    .HasForeignKey(d => d.SubTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Task_Sub");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
