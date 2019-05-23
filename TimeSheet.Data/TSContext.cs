using System;
using TimeSheet.Data.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TimeSheet.Data
{
    public partial class TSContext : DbContext
    {
        public TSContext()
        {
        }

        public TSContext(DbContextOptions<TSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TaskList> AppCompositeRole { get; set; }
        public virtual DbSet<Pocos.TimeSheet> AppCompositeRoleItem { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.ToTable("TaskList", "public");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TimeSheetId).HasColumnName("TimeSheetID");

                entity.Property(e => e.ProjectCode).HasMaxLength(20);

                entity.Property(e => e.TypeCode).HasMaxLength(20);

            });

            modelBuilder.Entity<Pocos.TimeSheet>(entity =>
            {
                entity.ToTable("TimeSheet", "public");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(255);

            });

        }
    }
}
