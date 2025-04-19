using ClinexSync.Application.Authentication;
using ClinexSync.Application.Data;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using ClinexSync.Domain.Users;
using ClinexSync.Infrastructure.Authentication;
using ClinexSync.Infrastructure.Authorization;
using ClinexSync.Infrastructure.Data;
using ClinexSync.Infrastructure.Data.Repositories;
using ClinexSync.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ClinexSync.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddServices().AddDatabase(configuration).AddAuthentication(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<
            Application.Authentication.IAuthenticationService,
            Authentication.AuthenticationService
        >();

        services.AddTransient<KeycloakClient>();

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddMemoryCache();

        services.AddHttpClient(
            "KeycloakExternal",
            (serviceProvider, client) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider
                    .GetRequiredService<IOptions<KeycloakOptions>>()
                    .Value;

                client.BaseAddress = new Uri(keycloakOptions.BaseUrl);
            }
        );

        services
            .AddHttpClient(
                "KeycloakInternal",
                (serviceProvider, client) =>
                {
                    KeycloakOptions keycloakOptions = serviceProvider
                        .GetRequiredService<IOptions<KeycloakOptions>>()
                        .Value;

                    client.BaseAddress = new Uri(keycloakOptions.BaseInternalUrl);
                }
            )
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

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
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IPersonRepository, PersonRepository>();

        services.AddTransient<ICityRepository, CityRepository>();

        services.AddTransient<IAreaRepository, AreaRepository>();

        services.AddTransient<IProfessionalRepository, ProfessionalRepository>();

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>()
        );

        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.AddAuthorization();

        services.Configure<KeycloakOptions>(configuration.GetSection("Authentication:Keycloak"));

        services.Configure<JwtOptions>(configuration.GetSection("Authentication:JwtOptions"));

        services.ConfigureOptions<JwtSetup>();

        services.AddTransient<IClaimsTransformation, RoleClaimTransformation>();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();

        return services;
    }
}
