using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Contracts.Areas;
using ClinexSync.Contracts.Persons;

namespace ClinexSync.Contracts.Professionals;

public sealed class ProfessionalResponse
{
    public Guid Id { get; set; }
    public string IdentityId { get; set; }
    public PersonResponse Person { get; set; }
    public IEnumerable<AreaResponse> Areas { get; set; }
}
