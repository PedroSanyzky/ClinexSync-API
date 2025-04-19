namespace ClinexSync.Contracts.Authentication;

public sealed class MeResponse
{
    public string IdentityId { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
