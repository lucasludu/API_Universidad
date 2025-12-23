using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.HasOne(e => e.ApplicationUser)
                           .WithMany()
                           .HasForeignKey(e => e.ApplicationUserId);

            builder.HasOne(e => e.Career)
                   .WithMany()
                   .HasForeignKey(e => e.CareerId);

            builder.Property(es => es.IsActive).HasDefaultValue(true);
        }
    }
}
