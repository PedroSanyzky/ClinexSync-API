using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.GetAreas;

public record GetProfessionalAreasQuery(Guid ProfessionalId)
    : IRequest<Result<IEnumerable<AreaResponse>>> { }
