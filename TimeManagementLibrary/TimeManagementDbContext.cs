using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeManagementLibrary
{
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<StudyTimeRecord> StudyTimeRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=TimeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Module>()
                .HasOne(m => m.User)
                .WithMany(u => u.Modules)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<StudyTimeRecord>()
                .HasOne(s => s.Module)
                .WithMany(m => m.StudyTimeRecords)
                .HasForeignKey(s => s.ModuleId);
        }
    }

}
