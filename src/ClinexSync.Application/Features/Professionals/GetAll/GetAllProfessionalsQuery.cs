using ClinexSync.Contracts.Professionals;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.GetAll;

public record GetAllProfessionalsQuery(string? FullName, Guid? AreaId, int PageNumber, int PageSize)
    : IRequest<Result<Paginated<BasicProfessionalResponse>>> { }
