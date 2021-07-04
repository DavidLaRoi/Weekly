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
        public virtual DbSet<BacklogItem> BacklogItems { get; set; }
        public virtual DbSet<BacklogItemCte> BacklogItemCtes { get; set; }
        public virtual DbSet<BiHasBi> BiHasBis { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
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

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Backlogs)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Backlog_Group");
            });

            modelBuilder.Entity<BacklogItem>(entity =>
            {
                entity.ToTable("BacklogItem");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BacklogId).HasColumnName("Backlog_ID");

                entity.Property(e => e.CompletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PriorityId).HasColumnName("Priority_ID");

                entity.HasOne(d => d.Backlog)
                    .WithMany(p => p.BacklogItems)
                    .HasForeignKey(d => d.BacklogId)
                    .HasConstraintName("FK_BacklogItem_Backlog");
            });

            modelBuilder.Entity<BacklogItemCte>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BacklogItem_CTE");

                entity.Property(e => e.ChildId).HasColumnName("ChildID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RootId).HasColumnName("RootID");
            });

            modelBuilder.Entity<BiHasBi>(entity =>
            {
                entity.ToTable("BI_Has_BI");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ChildId).HasColumnName("Child_ID");

                entity.Property(e => e.ParentId).HasColumnName("Parent_ID");

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.BiHasBiChildren)
                    .HasForeignKey(d => d.ChildId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BI_Has_BI_BacklogItem");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.BiHasBiParents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BI_Has_BI_BacklogItem1");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

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

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Task_Group");
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
                entity.ToTable("Task_Has_Task");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ChildTaskId).HasColumnName("ChildTask_ID");

                entity.Property(e => e.ParentTaskId).HasColumnName("ParentTask_ID");

                entity.HasOne(d => d.ChildTask)
                    .WithMany(p => p.TaskHasTaskChildTasks)
                    .HasForeignKey(d => d.ChildTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Child");

                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.TaskHasTaskParentTasks)
                    .HasForeignKey(d => d.ParentTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Has_Task_Parent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
