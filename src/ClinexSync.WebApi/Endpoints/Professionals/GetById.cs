using ClinexSync.Application.Features.Professionals.GetById;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Extensions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Professionals;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "professionals/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new GetProfessionalByIdQuery(id);

                    Result<ProfessionalResponse> result = await sender.Send(
                        command,
                        cancellationToken
                    );

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Professionals);
    }
}
