using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AspNetCoreWebApiProjManager.Database
{
    public partial class DbProjManagerContext : DbContext
    {
        public DbProjManagerContext() { }

        public DbProjManagerContext(DbContextOptions<DbProjManagerContext> options) : base(options) { }

        public virtual DbSet<TblProject> TblProjects { get; set; }
        public virtual DbSet<TblTask> TblTasks { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserpass> TblUserpasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ProjManagerDbConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblProject>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("PK_PROJECT");

                entity.ToTable("TBL_PROJECT");

                entity.Property(e => e.IdProject)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_PROJECT");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DETAILS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<TblTask>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("PK_TASK");

                entity.ToTable("TBL_TASK");

                entity.Property(e => e.IdTask)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TASK");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("date")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DETAILS");

                entity.Property(e => e.IdProject).HasColumnName("ID_PROJECT");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.TblTasks)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TASK_PROJECT");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.TblTasks)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TASK_USER");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_USER");

                entity.ToTable("TBL_USER");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_USER");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LAST_NAME");
            });

            modelBuilder.Entity<TblUserpass>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK_USERPASS");

                entity.ToTable("TBL_USERPASS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
