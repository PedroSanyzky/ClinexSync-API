using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.Create;

public record CreateProfessionalCommand(
    string FirstName,
    string LastName,
    string Phone,
    string DocumentNumber,
    string Email,
    string Street1,
    string Street2,
    string PostalCode,
    bool IsBis,
    int DoorNumber,
    DateOnly BirthDay,
    string Genre,
    Guid CityId,
    Guid DistrictId,
    Guid[] AreasToWork
) : IRequest<Result<Guid>> { }
