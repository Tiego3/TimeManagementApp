using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary
{
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<StudyTimeRecord> StudyTimeRecords { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Modules)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.StudyTimeRecords)
                .WithOne(s => s.Module)
                .HasForeignKey(s => s.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Semester>()
                .HasOne(s => s.User)
                .WithMany(u => u.Semesters) // ✅ Plural form
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
