using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Offices;

public sealed class Office : Entity
{
    private readonly List<Room> _rooms = [];

    private Office() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
}
