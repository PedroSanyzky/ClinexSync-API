# ClinexSync

**Backend ClinexSync – App para gestión de clínicas**

## Tecnologías

- **C# 10 / .NET 8**  
- **ASP.NET Core Minimal APIs** con **MediatR** (CQRS)  
- **Entity Framework Core** con migraciones automáticas  
- **Serilog** para logging estructurado  
- **Asp.Versioning** para control de versiones de la API  
- **Swagger** (Swashbuckle) para documentación interactiva  
- **Keycloak** + JWT para autenticación y autorización  
- **Docker** y **Docker Compose** para contenerización  

## Arquitectura

Se sigue el patrón **Clean Architecture**:

- **ClinexSync.Application**: lógica de negocio y casos de uso.  
- **ClinexSync.Contracts**: DTOs y contratos de la API.  
- **ClinexSync.Domain**: entidades y reglas del dominio.  
- **ClinexSync.Infrastructure**: repositorios, DbContext, servicios externos.  
- **ClinexSync.WebApi**: configuración de servicios y definición de endpoints.  

## Despliegue con Docker Compose

```bash
docker-compose up --build
