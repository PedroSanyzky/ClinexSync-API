using ClinexSync.Application.Features.Professionals.Create;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Shared;
using ClinexSync.WebApi.Extensions;
using ClinexSync.WebApi.Handlers;
using MediatR;

namespace ClinexSync.WebApi.Endpoints.Professionals;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "professionals",
                async (
                    CreateProfessionalRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var command = new CreateProfessionalCommand(
                        request.FirstName,
                        request.lastName,
                        request.phone,
                        request.documentNumber,
                        request.Email,
                        request.Street1,
                        request.Street2,
                        request.PostalCode,
                        request.IsBis,
                        request.DoorNumber,
                        request.BirthDay,
                        request.Genre,
                        request.cityId,
                        request.districtId,
                        request.AreasToWork
                    );

                    Result<Guid> result = await sender.Send(command, cancellationToken);

                    return ResultsHandler.CustomResponse(result);
                }
            )
            .WithTags(Tags.Professionals);
    }
}
