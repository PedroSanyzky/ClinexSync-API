using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Offices;

public sealed class Room : Entity
{
    private Room() { }

    public Guid Id { get; private set; }
    public string Prefix { get; private set; }
    public Guid OfficeId { get; private set; }
}
