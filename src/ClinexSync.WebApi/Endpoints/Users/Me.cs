using ClinexSync.Application.Features.Users.Me;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Users;

internal sealed class Me : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "users/me",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new MeQuery();

                    Result<MeResponse> result = await sender.Send(command, cancellationToken);

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
