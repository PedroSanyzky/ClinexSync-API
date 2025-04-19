using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("professionals");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Person).WithOne().HasForeignKey<Professional>(p => p.PersonId);

        builder.Property(p => p.IdentityId).IsRequired(false);

        builder.HasOne<User>().WithOne().HasForeignKey<Professional>(p => p.IdentityId);

        builder.HasIndex(p => p.IdentityId).IsUnique();

        builder.OwnsMany(
            p => p.AreasToWork,
            atw =>
            {
                atw.ToTable("professionalAreasToWork");

                atw.WithOwner().HasForeignKey("ProfessionalId");

                atw.HasKey("ProfessionalId", "Value");

                atw.Property(a => a.Value).HasColumnName("AreaToWorkId").ValueGeneratedNever();
            }
        );

        builder
            .Metadata.FindNavigation(nameof(Professional.AreasToWork))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
