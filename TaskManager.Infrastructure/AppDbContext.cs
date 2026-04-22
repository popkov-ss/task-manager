
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;

namespace TaskManagment.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobType> JobTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobType>().HasData(
                new JobType { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Работа"},
                new JobType { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Личное"},
                new JobType {Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Срочное" },
                new JobType {Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Идеи" }
            );

            modelBuilder.Entity<Job>()
                .HasIndex(t => t.IsCompleted);
            modelBuilder.Entity<Job>()
                .HasIndex(t => t.CreatedAt);
        }
    }
}
