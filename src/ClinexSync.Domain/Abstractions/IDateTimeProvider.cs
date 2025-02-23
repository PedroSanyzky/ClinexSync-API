namespace ClinexSync.Domain.Abstractions;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
