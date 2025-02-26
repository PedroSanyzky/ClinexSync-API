using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.GetAll;

public record GetAllProfessionalsQuery(
    int PageNumber,
    int PageSize,
    string? firstName,
    Guid? areaId
) : IRequest<Result<Paginated<BasicProfessionalResponse>>> { }
