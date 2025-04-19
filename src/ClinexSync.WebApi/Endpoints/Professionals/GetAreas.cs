using ClinexSync.Application.Features.Professionals.GetAreas;
using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Professionals;

internal sealed class GetAreas : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "professionals/{id:guid}/areas",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new GetProfessionalAreasQuery(id);

                    Result<IEnumerable<AreaResponse>> result = await sender.Send(
                        command,
                        cancellationToken
                    );

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Professionals)
            .RequireAuthorization();
    }
}
