using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Shared;
using MediatR;

namespace ClinexSync.Application.Services.Persons;

public class PersonValidationService : IPersonValidationService
{
    private readonly IPersonRepository _personRepository;

    public PersonValidationService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result> PersonExists(
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
        {
            return Result.Success();
        }

        return ValidatePerson(person, documentNumber, email, phone);
    }

    public Result<Person> CreatePerson(
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
        int DoorNumber
    )
    {
        Result<FirstName> firstNameResult = FirstName.Create(firstName);

        if (firstNameResult.IsFailure)
        {
            return Result.Failure<Person>(firstNameResult.Error);
        }

        Result<LastName> lastNameResult = LastName.Create(lastName);

        if (lastNameResult.IsFailure)
        {
            return Result.Failure<Person>(lastNameResult.Error);
        }

        Result<Phone> phoneResult = Phone.Create(phone);

        if (phoneResult.IsFailure)
        {
            return Result.Failure<Person>(phoneResult.Error);
        }

        Result<DocumentNumber> documentNumberResult = DocumentNumber.Create(documentNumber);

        if (documentNumberResult.IsFailure)
        {
            return Result.Failure<Person>(documentNumberResult.Error);
        }

        Result<Email> emailResult = Email.Create(email);

        if (emailResult.IsFailure)
        {
            return Result.Failure<Person>(emailResult.Error);
        }

        var address = new Address(Street1, Street2, City, PostalCode, District, IsBis, DoorNumber);

        Result<Person> personResult = Person.Create(
            firstNameResult.Value,
            lastNameResult.Value,
            phoneResult.Value,
            documentNumberResult.Value,
            emailResult.Value,
            address,
            birthDay,
            (Genre)Enum.Parse(typeof(Genre), genre),
            cityId,
            districtId
        );

        if (personResult.IsFailure)
        {
            return Result.Failure<Person>(personResult.Error);
        }

        return Result.Success(personResult.Value);
    }

    private Result ValidatePerson(Person person, string documentNumber, string email, string phone)
    {
        if (person.DocumentNumber.Value == documentNumber)
        {
            return Result.Failure(PersonErrors.DocumentNumberAlreadyExists());
        }

        if (person.Email.Value == email)
        {
            return Result.Failure(PersonErrors.EmailAlreadyExists());
        }

        if (person.Phone.Value == phone)
        {
            return Result.Failure(PersonErrors.PhoneAlreadyExists());
        }

        return Result.Success();
    }
}
