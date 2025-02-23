using MediatR;

namespace ClinexSync.WebApi.Endpoints.Pacients;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "pacients/{pacientId}",
                (Guid pacientId, ISender sender, CancellationToken cancellationToken) =>
                    Results.Ok("Lol")
            )
            .WithTags(Tags.Pacients);
    }
}
