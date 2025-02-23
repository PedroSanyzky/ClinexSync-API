using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Cities;

public sealed class District
{
    public Guid Id { get; private set; }
    public Guid CityId { get; private set; }
    public string Name { get; private set; }
}
