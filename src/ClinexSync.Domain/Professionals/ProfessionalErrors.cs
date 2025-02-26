using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Professionals;

public static class ProfessionalErrors
{
    public static Error NotFound(Guid professionalId) =>
        Error.NotFound(
            "Professional.NotFound",
            $"Professional item with the Id = '{professionalId}' was not found"
        );
}
