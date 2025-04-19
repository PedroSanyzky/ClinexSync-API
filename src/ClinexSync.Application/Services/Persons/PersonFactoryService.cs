using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Application.Services.Persons;

public class PersonFactoryService : IPersonFactoryService
{
    private readonly IPersonRepository _personRepository;

    public PersonFactoryService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<Person>> CreatePersonAsync(
        string firstName,
        string lastName,
        string phone,
        string documentNumber,
        string email,
        DateOnly birthDay,
        string genre,
        Guid cityId,
        Guid districtId,
        string Street1,
        string Street2,
        string City,
        string PostalCode,
        string District,
        bool IsBis,
        int DoorNumber,
        CancellationToken cancellationToken
    )
    {
        Result<FirstName> firstNameResult = FirstName.Create(firstName);
        Result<LastName> lastNameResult = LastName.Create(lastName);
        Result<Phone> phoneResult = Phone.Create(phone);
        Result<DocumentNumber> documentNumberResult = DocumentNumber.Create(documentNumber);
        Result<Email> emailResult = Email.Create(email);

        if (firstNameResult.IsFailure)
            return Result.Failure<Person>(firstNameResult.Error);

        if (lastNameResult.IsFailure)
            return Result.Failure<Person>(lastNameResult.Error);

        if (phoneResult.IsFailure)
            return Result.Failure<Person>(phoneResult.Error);

        if (documentNumberResult.IsFailure)
            return Result.Failure<Person>(documentNumberResult.Error);

        if (emailResult.IsFailure)
            return Result.Failure<Person>(emailResult.Error);

        Result existsResult = await PersonExistsAsync(
            email,
            documentNumber,
            phone,
            cancellationToken
        );

        if (existsResult.IsFailure)
            return Result.Failure<Person>(existsResult.Error);

        var address = new Address(Street1, Street2, City, PostalCode, District, IsBis, DoorNumber);

        Result<Person> personResult = Person.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            phoneResult.Value,
            documentNumberResult.Value,
            emailResult.Value,
            address,
            birthDay,
            Enum.Parse<Genre>(genre),
            cityId,
            districtId
        );

        if (personResult.IsFailure)
            return Result.Failure<Person>(personResult.Error);

        return Result.Success(personResult.Value);
    }

    private Result ValidatePerson(Person person, string documentNumber, string email, string phone)
    {
        if (person.DocumentNumber.Value == documentNumber)
            return Result.Failure(PersonErrors.DocumentNumberAlreadyExists());

        if (person.Email.Value == email)
            return Result.Failure(PersonErrors.EmailAlreadyExists());

        if (person.Phone.Value == phone)
            return Result.Failure(PersonErrors.PhoneAlreadyExists());

        return Result.Success();
    }

    private async Task<Result> PersonExistsAsync(
        string email,
        string documentNumber,
        string phone,
        CancellationToken cancellationToken
    )
    {
        Person person = await _personRepository.ExistsPerson(
            documentNumber,
            email,
            phone,
            cancellationToken
        );

        if (person is null)
            return Result.Success();

        return ValidatePerson(person, documentNumber, email, phone);
    }
}
