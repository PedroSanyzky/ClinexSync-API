using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Application.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterUserAsync(
        Person person,
        string password,
        CancellationToken cancellationToken = default
    );

    Task<Result<AuthorizationToken>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    );
}
