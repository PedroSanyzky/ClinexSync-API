using ClinexSync.Application.Features.Areas.Create;
using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Areas;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "areas",
                async (
                    CreateAreaRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var command = new CreateAreaCommand(request.Name);

                    Result<Guid> result = await sender.Send(command, cancellationToken);

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Areas)
            .RequireAuthorization();
    }
}
