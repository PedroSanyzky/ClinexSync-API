using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Areas;

public sealed class Area : Entity
{
    private Area(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    private Area() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<Area> Create(string name)
    {
        var area = new Area(Guid.NewGuid(), name);
        return Result.Success(area);
    }
}
