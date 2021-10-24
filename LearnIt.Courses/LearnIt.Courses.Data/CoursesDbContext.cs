using System;
using System.Threading;
using System.Threading.Tasks;
using LearnIt.Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnIt.Courses.Data
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Audit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTimeOffset.Now;
                        entry.Entity.Updated = DateTimeOffset.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Updated = DateTimeOffset.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}