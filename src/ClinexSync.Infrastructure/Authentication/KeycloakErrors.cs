using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Infrastructure.Authentication;

public static class KeycloakErrors
{
    public static Error InvalidCredentials() => Error.Problem("Auth.Failed", "Invalid credentials");
}
