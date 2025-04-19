namespace ClinexSync.Contracts.Authentication;

public record AccessTokenResponse(
    string AccessToken,
    int AccessTokenExpiresIn,
    string RefreshToken,
    int RefreshTokenExpiresIn
);
