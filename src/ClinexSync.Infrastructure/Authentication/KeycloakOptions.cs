namespace ClinexSync.Infrastructure.Authentication;

public sealed class KeycloakOptions
{
    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;

    public string BaseInternalUrl { get; init; } = string.Empty;
}
