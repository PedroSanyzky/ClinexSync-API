using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Application.Features.Users.Login;

public static class LoginErrors
{
    public static Error InvalidEmail() =>
        Error.Conflict("Auth.InvalidEmail", "Email is not valid.");

    public static Error InvalidPassword() =>
        Error.Conflict("Auth.InvalidPassword", "Password is not valid.");
}
