using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Cities;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class CityRepository(ApplicationDbContext dbContext) : ICityRepository
{
    public Task DeleteAsync(City entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Cities.Include(c => c.Districts)
            .FirstOrDefaultAsync(city => city.Id == id, cancellationToken);
    }

    public Task<IEnumerable<City>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(City entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(City entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
