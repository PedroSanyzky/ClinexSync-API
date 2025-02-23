using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Administrators;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Offices;
using ClinexSync.Domain.Ofices;
using ClinexSync.Domain.Pacients;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Pacient> Pacients { get; }
    DbSet<Administrator> Administrators { get; }
    DbSet<Professional> Professionals { get; }
    DbSet<Office> Offices { get; }
    DbSet<Room> Rooms { get; }
    DbSet<City> Cities { get; }
    DbSet<District> Districts { get; }
}
