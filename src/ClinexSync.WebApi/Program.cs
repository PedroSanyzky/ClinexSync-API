using System.Reflection;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using ClinexSync.Application;
using ClinexSync.Infrastructure;
using ClinexSync.WebApi;
using ClinexSync.WebApi.Endpoints;
using ClinexSync.WebApi.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation().AddInfrastructure(builder.Configuration).AddApplication();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Host.UseSerilog(
    (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration)
);

WebApplication app = builder.Build();

ApiVersionSet versionSet = app.NewApiVersionSet()
    .HasApiVersion(ApiVersions.V1, 0)
    .ReportApiVersions()
    .Build();

RouteGroupBuilder apiGroup = app.MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(versionSet);

app.MapEndpoints(apiGroup);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
