using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Application.Services.Persons;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Cities;
using ClinexSync.Domain.Professionals;
using ClinexSync.Domain.Shared;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.Create;

public class CreateProfessionalCommandHandler
    : IRequestHandler<CreateProfessionalCommand, Result<Guid>>
{
    private readonly IProfessionalRepository _professionalRepository;
    private readonly IPersonValidationService _personValidationService;
    private readonly ICityRepository _cityRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProfessionalCommandHandler(
        IProfessionalRepository professionalRepository,
        IPersonValidationService personValidationService,
        ICityRepository cityRepository,
        IAreaRepository areaRepository,
        IUnitOfWork unitOfWork
    )
    {
        _professionalRepository = professionalRepository;
        _personValidationService = personValidationService;
        _cityRepository = cityRepository;
        _areaRepository = areaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateProfessionalCommand request,
        CancellationToken cancellationToken
    )
    {
        City? city = await _cityRepository.GetByIdAsync(request.cityId, cancellationToken);

        if (city is null)
        {
            return Result.Failure<Guid>(CityErrors.CityNotFound(request.cityId));
        }

        District? district = city.Districts.FirstOrDefault(d => d.Id == request.districtId);

        if (district is null)
        {
            return Result.Failure<Guid>(CityErrors.DistrictNotFound(request.districtId));
        }

        Result<Person> personResult = _personValidationService.CreatePerson(
            request.FirstName,
            request.LastName,
            request.phone,
            request.documentNumber,
            request.Email,
            request.BirthDay,
            request.Genre,
            request.cityId,
            request.districtId,
            request.Street1,
            request.Street2,
            city.Name,
            request.PostalCode,
            district.Name,
            request.IsBis,
            request.DoorNumber
        );

        if (personResult.IsFailure)
        {
            return Result.Failure<Guid>(personResult.Error);
        }

        Person person = personResult.Value;

        Result personExistsResult = await _personValidationService.PersonExists(
            person.Email.Value,
            person.DocumentNumber.Value,
            person.Phone.Value,
            cancellationToken
        );

        if (personExistsResult.IsFailure)
        {
            return Result.Failure<Guid>(personExistsResult.Error);
        }

        Result<Professional> professionalResult = Professional.Create(person);

        if (professionalResult.IsFailure)
        {
            return Result.Failure<Guid>(professionalResult.Error);
        }

        Professional professional = professionalResult.Value;

        foreach (Guid areaId in request.AreasToWork)
        {
            Area? area = await _areaRepository.GetByIdAsync(areaId, cancellationToken);

            if (area is null)
            {
                return Result.Failure<Guid>(AreaErrors.NotFound(areaId));
            }

            professional.AddAreaToWorkId(area.Id);
        }

        await _professionalRepository.InsertAsync(professional, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(professional.Id);
    }
}
