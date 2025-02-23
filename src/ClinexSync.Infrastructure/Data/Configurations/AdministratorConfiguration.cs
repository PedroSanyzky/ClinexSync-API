using ClinexSync.Domain.Administrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable("administrators");

        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Person).WithOne().HasForeignKey<Administrator>(a => a.PersonId);

        builder.Property(a => a.IdentityId).IsRequired(false);

        builder.HasIndex(a => a.IdentityId).IsUnique();
    }
}
