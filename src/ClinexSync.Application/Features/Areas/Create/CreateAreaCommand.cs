using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Areas.Create;

public record CreateAreaCommand(string Name) : IRequest<Result<Guid>> { }
