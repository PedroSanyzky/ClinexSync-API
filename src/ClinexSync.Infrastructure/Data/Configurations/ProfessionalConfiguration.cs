using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
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

        builder.HasMany<Area>(p => p.AreasToWork).WithMany();

        builder.Property(p => p.IdentityId).IsRequired(false);

        builder.HasIndex(p => p.IdentityId).IsUnique();
    }
}
