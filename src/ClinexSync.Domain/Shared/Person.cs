using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public class Person : Entity
{
    private Person() { }

    private Person(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Phone phone,
        DocumentNumber documentNumber,
        Email email,
        Address address,
        DateOnly birthDay,
        Genre genre,
        Guid cityId,
        Guid districtId
    )
    {
        PersonId = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        DocumentNumber = documentNumber;
        Email = email;
        Address = address;
        BirthDay = birthDay;
        Genre = genre;
        CityId = cityId;
        DistrictId = districtId;
    }

    public Guid PersonId { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Phone Phone { get; private set; }
    public DocumentNumber DocumentNumber { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public DateOnly BirthDay { get; private set; }
    public Genre Genre { get; private set; }
    public Guid CityId { get; private set; }
    public Guid DistrictId { get; private set; }

    public static Result<Person> Create(
        FirstName firstName,
        LastName lastName,
        Phone phone,
        DocumentNumber documentNumber,
        Email email,
        Address address,
        DateOnly birthDay,
        Genre genre,
        Guid cityId,
        Guid districtId
    )
    {
        var person = new Person(
            Guid.NewGuid(),
            firstName,
            lastName,
            phone,
            documentNumber,
            email,
            address,
            birthDay,
            genre,
            cityId,
            districtId
        );

        return Result.Success(person);
    }
}
