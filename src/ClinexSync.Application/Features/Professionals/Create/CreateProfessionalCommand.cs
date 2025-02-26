using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.Create;

public record CreateProfessionalCommand(
    string FirstName,
    string LastName,
    string phone,
    string documentNumber,
    string Email,
    string Street1,
    string Street2,
    string PostalCode,
    bool IsBis,
    int DoorNumber,
    DateOnly BirthDay,
    string Genre,
    Guid cityId,
    Guid districtId,
    Guid[] AreasToWork
) : IRequest<Result<Guid>> { }
