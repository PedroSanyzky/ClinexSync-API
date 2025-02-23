using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Abstractions;

public abstract class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string Pacient = nameof(Pacient);
    public const string Professional = nameof(Professional);
}
