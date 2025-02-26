using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinexSync.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(p => p.PersonId);

        builder.OwnsOne(
            p => p.FirstName,
            f => f.Property(f => f.Value).IsRequired().HasColumnName("FirstName")
        );

        builder.OwnsOne(
            p => p.LastName,
            l => l.Property(l => l.Value).IsRequired().HasColumnName("LastName")
        );

        builder.OwnsOne(
            p => p.Phone,
            p =>
            {
                p.Property(phone => phone.Value).IsRequired().HasColumnName("Phone");
                p.HasIndex(phone => phone.Value).IsUnique();
            }
        );

        builder.OwnsOne(
            p => p.DocumentNumber,
            d =>
            {
                d.Property(d => d.Value).IsRequired().HasColumnName("DocumentNumber");
                d.HasIndex(d => d.Value).IsUnique();
            }
        );

        builder.OwnsOne(
            p => p.Email,
            e =>
            {
                e.Property(email => email.Value).IsRequired().HasColumnName("Email");
                e.HasIndex(email => email.Value).IsUnique();
            }
        );

        builder.Property(p => p.BirthDay).IsRequired();

        builder.Property(p => p.Genre).IsRequired().HasConversion<string>();

        builder.OwnsOne(p => p.Address);

        builder.HasOne<City>().WithMany().HasForeignKey(p => p.CityId);

        builder
            .HasOne<District>()
            .WithMany()
            .HasForeignKey(p => p.DistrictId)
            .OnDelete(DeleteBehavior.NoAction);
        ;
    }
}

//EF Conversions ??

//builder
//    .Property(p => p.FirstName)
//    .HasConversion(firstName => firstName.Value, value => FirstName.Create(value).Value)
//    .IsRequired();

//builder
//    .Property(p => p.LastName)
//    .HasConversion(lastName => lastName.Value, value => LastName.Create(value).Value)
//    .IsRequired();

//builder
//    .Property(p => p.Phone)
//    .HasConversion(phone => phone.Value, value => Phone.Create(value).Value)
//    .IsRequired();

//builder
//    .Property(p => p.DocumentNumber)
//    .HasConversion(document => document.Value, value => DocumentNumber.Create(value).Value)
//    .IsRequired();

//builder
//    .Property(p => p.Email)
//    .HasConversion(email => email.Value, value => Email.Create(value).Value)
//    .IsRequired();
