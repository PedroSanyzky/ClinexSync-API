using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Application.Services.Persons;

public interface IPersonFactoryService
{
    Task<Result<Person>> CreatePersonAsync(
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
    );
}
