using ClinexSync.Contracts.Persons;

namespace ClinexSync.Contracts.Professionals;

public sealed class ProfessionalResponse
{
    public Guid Id { get; set; }
    public string IdentityId { get; set; }
    public PersonResponse Person { get; set; }
}
