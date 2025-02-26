using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Application.Data;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using ClinexSync.Infrastructure.Data;
using ClinexSync.Infrastructure.Data.Repositories;
using ClinexSync.Infrastructure.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClinexSync.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddServices().AddDatabase(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string connectionString =
            configuration.GetConnectionString("database")
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<IAreaRepository, AreaRepository>();
        services.AddTransient<IProfessionalRepository, ProfessionalRepository>();

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>()
        );

        return services;
    }
}
