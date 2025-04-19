using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Infrastructure.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => UtcNow;
}
