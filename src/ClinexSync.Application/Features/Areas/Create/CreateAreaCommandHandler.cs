using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using MediatR;

namespace ClinexSync.Application.Features.Areas.Create;

public class CreateAreaCommandHandler : IRequestHandler<CreateAreaCommand, Result<Guid>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAreaCommandHandler(IAreaRepository areaRepository, IUnitOfWork unitOfWork)
    {
        _areaRepository = areaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateAreaCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _areaRepository.ExistsAsync(request.Name, cancellationToken))
        {
            return Result.Failure<Guid>(AreaErrors.AlreadyExists(request.Name));
        }

        Result<Area> areaResult = Area.Create(request.Name);

        if (areaResult.IsFailure)
        {
            return Result.Failure<Guid>(areaResult.Error);
        }

        Area area = areaResult.Value;

        await _areaRepository.InsertAsync(area, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(area.Id);
    }
}
