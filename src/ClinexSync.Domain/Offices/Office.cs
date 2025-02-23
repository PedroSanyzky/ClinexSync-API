using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Offices;

public sealed class Office : Entity
{
    private Office() { }

    private readonly IList<Room> _rooms = [];
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
