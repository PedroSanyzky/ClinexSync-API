using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Areas;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class AreaRepository(ApplicationDbContext dbContext) : IAreaRepository
{
    public Task DeleteAsync(Area entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    public Task InsertAsync(Area entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Area entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
