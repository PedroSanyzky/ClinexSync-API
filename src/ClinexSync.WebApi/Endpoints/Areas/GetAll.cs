using ClinexSync.Application.Features.Areas.GetAll;
using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Areas;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "areas",
                async (string? name, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new GetAllAreasQuery(name);

                    Result<IEnumerable<AreaResponse>> result = await sender.Send(
                        command,
                        cancellationToken
                    );

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Areas)
            .RequireAuthorization();
    }
}
