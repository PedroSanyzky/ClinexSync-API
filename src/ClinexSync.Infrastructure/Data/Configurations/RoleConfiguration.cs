using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(role => role.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.HasIndex(role => role.Name).IsUnique();
        builder.HasData([Role.Pacient, Role.Professional, Role.Administrator]);
    }
}
