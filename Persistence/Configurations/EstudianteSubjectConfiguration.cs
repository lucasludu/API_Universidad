using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EstudianteSubjectConfiguration : IEntityTypeConfiguration<EstudianteSubject>
    {
        public void Configure(EntityTypeBuilder<EstudianteSubject> builder)
        {
            builder.HasKey(es => new { es.EstudianteId, es.SubjectId });

            builder.HasOne(es => es.Estudiante)
                .WithMany(e => e.EstudianteSubjects)
                .HasForeignKey(es => es.EstudianteId);

            builder.HasOne(es => es.Subject)
                .WithMany()
                .HasForeignKey(es => es.SubjectId);
        }
    }
}
