namespace ClinexSync.Domain.Cities;

public sealed class District
{
    public Guid Id { get; private set; }
    public Guid CityId { get; private set; }
    public string Name { get; private set; }
}
