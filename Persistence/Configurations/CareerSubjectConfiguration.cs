using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CareerSubjectConfiguration : IEntityTypeConfiguration<CareerSubject>
    {
        public void Configure(EntityTypeBuilder<CareerSubject> builder)
        {
            builder.HasKey(cs => new { cs.CareerId, cs.SubjectId });

            builder.HasOne(cs => cs.Career)
                .WithMany(c => c.CareerSubjects)
                .HasForeignKey(cs => cs.CareerId);

            builder.HasOne(cs => cs.Subject)
                .WithMany(s => s.CareerSubjects)
                .HasForeignKey(cs => cs.SubjectId);
        }
    }
}
