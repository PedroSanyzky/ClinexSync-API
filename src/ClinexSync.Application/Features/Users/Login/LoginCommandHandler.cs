using ClinexSync.Application.Authentication;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AccessTokenResponse>>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken
    )
    {
        Result<AuthorizationToken> loginResult = await _authenticationService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken
        );

        if (loginResult.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(loginResult.Error);
        }

        AuthorizationToken token = loginResult.Value;

        return new AccessTokenResponse(
            token.AccessToken,
            token.AccessTokenExpiresIn,
            token.RefreshToken,
            token.RefreshTokenExpiresIn
        );
    }
}
