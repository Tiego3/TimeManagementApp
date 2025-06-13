using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary
{
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Semester> Semesters => Set<Semester>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<StudyTimeRecord> StudyTimeRecords => Set<StudyTimeRecord>();

        public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            mb.Entity<User>()
                .HasOne(u => u.Semester)
                .WithOne(s => s.User)
                .HasForeignKey<Semester>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Semester>()
                .HasMany(s => s.Modules)
                .WithOne(m => m.Semester)
                .HasForeignKey(m => m.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Module>()
                .HasMany(m => m.StudyTimeRecords)
                .WithOne(r => r.Module)
                .HasForeignKey(r => r.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


    //  public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> options)
    //     : base(options) { }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<User>()
    //        .HasIndex(u => u.Username)
    //        .IsUnique();

    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.Modules)
    //        .WithOne(m => m.User)
    //        .HasForeignKey(m => m.UserId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    modelBuilder.Entity<Module>()
    //        .HasMany(m => m.StudyTimeRecords)
    //        .WithOne(s => s.Module)
    //        .HasForeignKey(s => s.ModuleId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    modelBuilder.Entity<Semester>()
    //        .HasOne(s => s.User)
    //        .WithMany(u => u.Semesters) 
    //        .HasForeignKey(s => s.UserId)
    //        .OnDelete(DeleteBehavior.Cascade);


    //    base.OnModelCreating(modelBuilder);
    //}
}

