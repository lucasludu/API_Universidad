using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProfesorSubjectConfiguration : IEntityTypeConfiguration<ProfesorSubject>
    {
        public void Configure(EntityTypeBuilder<ProfesorSubject> builder)
        {
            builder.HasKey(ps => new { ps.ProfesorId, ps.SubjectId });

            builder.HasOne(ps => ps.Profesor)
                   .WithMany(p => p.ProfesorSubjects)
                   .HasForeignKey(ps => ps.ProfesorId);

            builder.HasOne(ps => ps.Subject)
                   .WithMany()
                   .HasForeignKey(ps => ps.SubjectId);
        }
    }
}
