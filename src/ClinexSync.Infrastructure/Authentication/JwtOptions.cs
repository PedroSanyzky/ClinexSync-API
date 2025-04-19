namespace ClinexSync.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public string Audience { get; set; } = string.Empty;

    public string MetadataUrl { get; set; } = string.Empty;

    public bool RequireHttpsMetadata { get; init; }

    public string Issuer { get; set; } = string.Empty;
}
