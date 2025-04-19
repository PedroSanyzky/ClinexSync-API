using ClinexSync.Domain.Professionals;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class ProfessionalRepository(ApplicationDbContext dbContext) : IProfessionalRepository
{
    public Task DeleteAsync(Professional entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Professional>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Professional?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Professional>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(Professional entity, CancellationToken cancellationToken)
    {
        await dbContext.Professionals.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(Professional entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
