using ClinexSync.Domain.Areas;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class AreaRepository(ApplicationDbContext dbContext) : IAreaRepository
{
    public Task DeleteAsync(Area entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.FirstOrDefaultAsync(
            a => a.Name.ToLower().Equals(name.ToLower()),
            cancellationToken
        )
            is not null;
    }

    public Task<IEnumerable<Area>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Area?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Areas.FirstOrDefaultAsync(area => area.Id == id, cancellationToken);
    }

    public Task<IEnumerable<Area>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(Area entity, CancellationToken cancellationToken)
    {
        await dbContext.Areas.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(Area entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
