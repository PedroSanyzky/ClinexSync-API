using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Users.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<AccessTokenResponse>>;
