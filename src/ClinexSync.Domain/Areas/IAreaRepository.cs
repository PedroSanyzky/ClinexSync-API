using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Areas;

public interface IAreaRepository : IRepository<Area, Guid>
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
}
