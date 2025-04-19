using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Areas;

public static class AreaErrors
{
    public static Error NotFound(Guid areaId) =>
        Error.NotFound("Area.NotFound", $"Area item with the Id = '{areaId}' was not found");

    public static Error AlreadyExists(string name) =>
        Error.NotFound("Area.AlreadyExists", $"Area with name = '{name}' already exists");

    public static Error NameEmpty() =>
        Error.Validation("Area.InvalidName", "Name cannot be empty.");
}
