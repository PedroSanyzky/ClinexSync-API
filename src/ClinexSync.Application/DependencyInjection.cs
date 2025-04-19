using ClinexSync.Application.Behaviors;
using ClinexSync.Application.Services.Persons;
using ClinexSync.Application.Services.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ClinexSync.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly,
            includeInternalTypes: true
        );

        services.AddTransient<IPersonFactoryService, PersonFactoryService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
