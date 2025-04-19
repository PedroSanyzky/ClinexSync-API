using ClinexSync.Contracts.Professionals;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.GetById;

public record GetProfessionalByIdQuery(Guid ProfessionalId)
    : IRequest<Result<ProfessionalResponse>> { }
