using ClinexSync.Application.Features.Professionals.GetAll;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Professionals;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "professionals",
                async (
                    int pageNumber,
                    int pageSize,
                    string? firstName,
                    Guid? areaId,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var command = new GetAllProfessionalsQuery(
                        firstName,
                        areaId,
                        pageNumber,
                        pageSize
                    );

                    Result<Paginated<BasicProfessionalResponse>> result = await sender.Send(
                        command,
                        cancellationToken
                    );

                    return ResultsHandler.CustomPaginatedResponse(result);
                }
            )
            .WithTags(Tags.Professionals)
            .RequireAuthorization();
    }
}
