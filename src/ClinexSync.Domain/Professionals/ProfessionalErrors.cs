using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Professionals;

public static class ProfessionalErrors
{
    public static Error NotFound(Guid professionalId) =>
        Error.NotFound(
            "Professional.NotFound",
            $"Professional item with the Id = '{professionalId}' was not found"
        );

    public static Error AreaAlreadyAssigned(string areaName) =>
        Error.Conflict(
            "Professional.AreaAlreadyAssigned",
            $"The professional has already been assigned the area {areaName}"
        );
}
