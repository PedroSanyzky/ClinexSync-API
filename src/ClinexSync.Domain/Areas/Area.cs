using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Areas;

public sealed class Area : Entity
{
    private Area() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
