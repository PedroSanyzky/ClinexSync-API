using ClinexSync.Application.Features.Users.Login;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "users/login",
                async (LoginRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new LoginCommand(request.email, request.password);

                    Result<AccessTokenResponse> result = await sender.Send(
                        command,
                        cancellationToken
                    );

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Users);
    }
}
