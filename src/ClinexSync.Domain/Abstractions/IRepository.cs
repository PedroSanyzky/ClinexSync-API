namespace ClinexSync.Domain.Abstractions;

public interface IRepository<T, TId>
{
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task InsertAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken
    );
}
