﻿using System;
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

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<TaskList> TaskList { get; set; }
        public virtual DbSet<Pocos.TimeSheet> TimeSheet { get; set; }

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

        }
    }
}
