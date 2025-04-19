using System.Text.Json.Serialization;

namespace ClinexSync.Contracts.Authentication;

public sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int AccessTokenExpiresIn { get; init; } = 0;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = string.Empty;

    [JsonPropertyName("refresh_expires_in")]
    public int RefreshTokenExpiresIn { get; init; } = 0;
}
