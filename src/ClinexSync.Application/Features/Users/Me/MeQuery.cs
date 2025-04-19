using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Users.Me;

public record MeQuery() : IRequest<Result<MeResponse>> { }
