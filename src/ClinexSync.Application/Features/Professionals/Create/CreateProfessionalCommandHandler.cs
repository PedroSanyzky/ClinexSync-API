using ClinexSync.Application.Services.Persons;
using ClinexSync.Application.Services.Users;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using ClinexSync.Domain.Users;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.Create;

public class CreateProfessionalCommandHandler
    : IRequestHandler<CreateProfessionalCommand, Result<Guid>>
{
    private readonly IProfessionalRepository _professionalRepository;
    private readonly IPersonFactoryService _personFactoryService;
    private readonly ICityRepository _cityRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProfessionalCommandHandler(
        IProfessionalRepository professionalRepository,
        IPersonFactoryService personValidationService,
        ICityRepository cityRepository,
        IAreaRepository areaRepository,
        IUserService userService,
        IUnitOfWork unitOfWork
    )
    {
        _professionalRepository = professionalRepository;
        _personFactoryService = personValidationService;
        _cityRepository = cityRepository;
        _areaRepository = areaRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<Result<Guid>> Handle(
        CreateProfessionalCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationCitiesResult = await ValidateCitiesAsync(
            request.CityId,
            request.DistrictId,
            cancellationToken
        );

        if (validationCitiesResult.IsFailure)
            return Result.Failure<Guid>(validationCitiesResult.Error);

        var (city, district) = validationCitiesResult.Value;

        Result<Person> personResult = await _personFactoryService.CreatePersonAsync(
            request.FirstName,
            request.LastName,
            request.Phone,
            request.DocumentNumber,
            request.Email,
            request.BirthDay,
            request.Genre,
            request.CityId,
            request.DistrictId,
            request.Street1,
            request.Street2,
            city.Name,
            request.PostalCode,
            district.Name,
            request.IsBis,
            request.DoorNumber,
            cancellationToken
        );

        if (personResult.IsFailure)
            return Result.Failure<Guid>(personResult.Error);

        Result<Professional> professionalResult = Professional.Create(personResult.Value);

        if (professionalResult.IsFailure)
            return Result.Failure<Guid>(professionalResult.Error);

        Result addAreasResult = await AddAreasToProfessionalAsync(
            request.AreasToWork,
            professionalResult.Value,
            cancellationToken
        );

        if (addAreasResult.IsFailure)
            return Result.Failure<Guid>(addAreasResult.Error);

        Result<User> userResult = await _userService.CreateUserAsync(
            personResult.Value,
            Role.Professional,
            cancellationToken
        );

        if (userResult.IsFailure)
            return Result.Failure<Guid>(userResult.Error);

        professionalResult.Value.SetIdentityId(userResult.Value.IdentityId);

        await _professionalRepository.InsertAsync(professionalResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(professionalResult.Value.Id);
    }

    private async Task<Result<(City, District)>> ValidateCitiesAsync(
        Guid cityId,
        Guid districtId,
        CancellationToken cancellationToken
    )
    {
        City? city = await _cityRepository.GetByIdAsync(cityId, cancellationToken);

        if (city is null)
            return Result.Failure<(City, District)>(CityErrors.CityNotFound(cityId));

        District? district = city.Districts.FirstOrDefault(d => d.Id == districtId);

        if (district is null)
            return Result.Failure<(City, District)>(CityErrors.DistrictNotFound(districtId));

        return Result.Success((city, district));
    }

    private async Task<Result> AddAreasToProfessionalAsync(
        IEnumerable<Guid> areasToWork,
        Professional professional,
        CancellationToken cancellationToken
    )
    {
        foreach (Guid areaId in areasToWork)
        {
            Area? area = await _areaRepository.GetByIdAsync(areaId, cancellationToken);

            if (area is null)
                return Result.Failure(AreaErrors.NotFound(areaId));

            Result<AreaToWorkId> addAreaResult = professional.AddAreaToWork(area);

            if (addAreaResult.IsFailure)
                return Result.Failure(addAreaResult.Error);
        }

        return Result.Success();
    }
}
