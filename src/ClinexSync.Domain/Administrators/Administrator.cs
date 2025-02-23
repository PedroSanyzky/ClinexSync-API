using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Administrators;

public sealed class Administrator
{
    private Administrator(Guid id, Person person)
    {
        Id = id;
        Person = person;
        PersonId = person.PersonId;
    }

    private Administrator() { }

    public Guid Id { get; private set; }
    public Guid PersonId { get; private set; }
    public Person Person { get; private set; }
    public string IdentityId { get; private set; }

    public static Result<Administrator> Create(Person person)
    {
        var administrator = new Administrator(Guid.NewGuid(), person);
        return Result.Success(administrator);
    }
}
