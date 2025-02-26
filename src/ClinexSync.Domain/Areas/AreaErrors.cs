using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Areas;

public static class AreaErrors
{
    public static Error NotFound(Guid areaId) =>
        Error.NotFound("Area.NotFound", $"Area item with the Id = '{areaId}' was not found");
}
