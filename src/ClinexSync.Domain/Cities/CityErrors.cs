using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Cities;

public static class CityErrors
{
    public static Error CityNotFound(Guid cityId) =>
        Error.NotFound("City.NotFound", $"City item with the Id = '{cityId}' was not found");

    public static Error DistrictNotFound(Guid districtId) =>
        Error.NotFound(
            "DistrictId.NotFound",
            $"District item with the Id = '{districtId}' was not found"
        );
}
