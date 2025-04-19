# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ClinexSync.WebApi/ClinexSync.WebApi.csproj", "src/ClinexSync.WebApi/"]
COPY ["src/ClinexSync.Contracts/ClinexSync.Contracts.csproj", "src/ClinexSync.Contracts/"]
COPY ["src/ClinexSync.Domain/ClinexSync.Domain.csproj", "src/ClinexSync.Domain/"]
COPY ["src/ClinexSync.Infrastructure/ClinexSync.Infrastructure.csproj", "src/ClinexSync.Infrastructure/"]
COPY ["src/ClinexSync.Application/ClinexSync.Application.csproj", "src/ClinexSync.Application/"]
RUN dotnet restore "./src/ClinexSync.WebApi/ClinexSync.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ClinexSync.WebApi"
RUN dotnet build "./ClinexSync.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClinexSync.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinexSync.WebApi.dll"]