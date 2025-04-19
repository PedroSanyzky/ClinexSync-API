using ClinexSync.Application.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;
using ClinexSync.Domain.Users;

namespace ClinexSync.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;

    public UserService(IAuthenticationService authenticationService, IUserRepository userRepository)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
    }

    public async Task<Result<User>> CreateUserAsync(
        Person person,
        Role role,
        CancellationToken cancellationToken
    )
    {
        Result<string> identityResult = await _authenticationService.RegisterUserAsync(
            person,
            person.DocumentNumber.Value,
            cancellationToken
        );

        if (identityResult.IsFailure)
            return Result.Failure<User>(identityResult.Error);

        User user = User.Create(identityResult.Value, role, person);

        await _userRepository.InsertAsync(user, cancellationToken);

        return Result.Success(user);
    }

    public async Task<Role?> GetUserRoleAsync(string identityId)
    {
        return await _userRepository.GetUserRoleById(identityId);
    }
}
