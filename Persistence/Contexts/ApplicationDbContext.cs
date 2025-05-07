using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDateTimeService datetime)
        : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this._dateTime = datetime;
        }

        public DbSet<Career> Careers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CareerSubject> CareerSubjects { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CareerSubject>()
                .HasKey(cs => new { cs.CareerId, cs.SubjectId });
            modelBuilder.Entity<CareerSubject>()
                .HasOne(cs => cs.Career)
                .WithMany(c => c.CareerSubjects)
                .HasForeignKey(cs => cs.CareerId);
            modelBuilder.Entity<CareerSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.CareerSubjects)
                .HasForeignKey(cs => cs.SubjectId);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
