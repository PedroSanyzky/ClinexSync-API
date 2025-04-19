using ClinexSync.Domain.Pacients;
using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class PacientConfiguration : IEntityTypeConfiguration<Pacient>
{
    public void Configure(EntityTypeBuilder<Pacient> builder)
    {
        builder.ToTable("pacients");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Person).WithOne().HasForeignKey<Pacient>(p => p.PersonId);

        builder.Property(p => p.IdentityId).IsRequired(false);

        builder.HasOne<User>().WithOne().HasForeignKey<Pacient>(p => p.IdentityId);

        builder.HasIndex(p => p.IdentityId).IsUnique();
    }
}
