using ClinexSync.Application.Authentication;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Infrastructure.Authentication;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly KeycloakClient _keycloakClient;

    public AuthenticationService(KeycloakClient keycloakClient)
    {
        _keycloakClient = keycloakClient;
    }

    public async Task<Result<AuthorizationToken>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    )
    {
        return await _keycloakClient.LoginAsync(email, password, cancellationToken);
    }

    public async Task<Result<string>> RegisterUserAsync(
        Person person,
        string password,
        CancellationToken cancellationToken = default
    )
    {
        return await _keycloakClient.RegisterAsync(person, password, cancellationToken);
    }
}
