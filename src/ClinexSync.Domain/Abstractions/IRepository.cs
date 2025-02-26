using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Abstractions;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
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
