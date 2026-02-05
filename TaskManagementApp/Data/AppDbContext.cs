using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaskManagementApp.Models;

namespace TaskManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Indexes for faster search
            modelBuilder.Entity<TaskItem>()
                .HasIndex(t => t.Title);

            modelBuilder.Entity<TaskItem>()
                .HasIndex(t => t.Status);
        }
    }
}
