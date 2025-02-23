using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Pacients;

public sealed class Pacient
{
    private Pacient(Guid id, Person person)
    {
        Id = id;
        Person = person;
        PersonId = person.PersonId;
    }

    private Pacient() { }

    public Guid Id { get; private set; }
    public Guid PersonId { get; set; }
    public Person Person { get; private set; }
    public string IdentityId { get; private set; }

    public static Result<Pacient> Create(Person person)
    {
        var pacient = new Pacient(Guid.NewGuid(), person);

        return Result.Success(pacient);
    }
}
