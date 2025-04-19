namespace ClinexSync.Domain.Cities;

public sealed class City
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<District> Districts { get; private set; } = [];
}
