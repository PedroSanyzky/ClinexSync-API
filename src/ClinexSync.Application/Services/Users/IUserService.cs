using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;
using ClinexSync.Domain.Users;

namespace ClinexSync.Application.Services.Users;

public interface IUserService
{
    Task<Result<User>> CreateUserAsync(
        Person person,
        Role role,
        CancellationToken cancellationToken
    );

    Task<Role?> GetUserRoleAsync(string identityId);
}
