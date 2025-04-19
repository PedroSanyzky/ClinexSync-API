using ClinexSync.Domain.Administrators;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Offices;
using ClinexSync.Domain.Pacients;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Pacient> Pacients { get; }
    DbSet<Administrator> Administrators { get; }
    DbSet<Professional> Professionals { get; }
    DbSet<Office> Offices { get; }
    DbSet<Room> Rooms { get; }
    DbSet<City> Cities { get; }
    DbSet<District> Districts { get; }
    DbSet<Area> Areas { get; }
}
