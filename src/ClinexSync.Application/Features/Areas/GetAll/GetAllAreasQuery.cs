using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Areas.GetAll;

public record GetAllAreasQuery(string? Name) : IRequest<Result<IEnumerable<AreaResponse>>> { }
