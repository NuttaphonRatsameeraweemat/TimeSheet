using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TimeSheet.Data.Pocos
{
    public partial class ddtk6ven76jfb4Context : DbContext
    {
        public ddtk6ven76jfb4Context()
        {
        }

        public ddtk6ven76jfb4Context(DbContextOptions<ddtk6ven76jfb4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<TaskList> TaskList { get; set; }
        public virtual DbSet<TimeSheet> TimeSheet { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=ddtk6ven76jfb4;Username=postgres;Password=Admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("Employee_pkey");

                entity.Property(e => e.Email).ValueGeneratedNever();
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => new { e.Email, e.Password1 })
                    .HasName("Password_pkey");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.Email, e.RoleId })
                    .HasName("UserRole_pkey");
            });
        }
    }
}
