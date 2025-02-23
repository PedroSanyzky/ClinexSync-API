using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Professionals;

public sealed class Professional
{
    private Professional(Guid id, Person person)
    {
        Id = id;
        Person = person;
        PersonId = person.PersonId;
    }

    private Professional() { }

    public Guid Id { get; private set; }
    public Guid PersonId { get; set; }
    public Person Person { get; private set; }
    public string IdentityId { get; private set; }

    private readonly IList<Guid> _areasToWork = [];
    public IReadOnlyList<Guid> AreasToWork => _areasToWork.AsReadOnly();

    public static Result<Professional> Create(Person person)
    {
        var professional = new Professional(Guid.NewGuid(), person);

        return Result.Success(professional);
    }
}
