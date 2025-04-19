using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.IdentityId);
        builder.HasIndex(user => user.IdentityId).IsUnique();
        builder.HasOne(user => user.Role).WithMany().HasForeignKey(u => u.RoleId);
    }
}
