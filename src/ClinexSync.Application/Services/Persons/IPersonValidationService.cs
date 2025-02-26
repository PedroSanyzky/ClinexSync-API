using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Application.Services.Persons;

public interface IPersonValidationService
{
    Task<Result> PersonExists(
        string email,
        string documentNumber,
        string phone,
        CancellationToken cancellationToken
    );

    Result<Person> CreatePerson(
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
    );
}
