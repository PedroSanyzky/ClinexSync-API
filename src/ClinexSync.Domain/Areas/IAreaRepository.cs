using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Areas;

public interface IAreaRepository : IRepository<Area>
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
}
