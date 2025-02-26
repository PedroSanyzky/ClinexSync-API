using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Application.Behaviors;
using ClinexSync.Application.Services.Persons;
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

        services.AddTransient<IPersonValidationService, PersonValidationService>();

        return services;
    }
}
