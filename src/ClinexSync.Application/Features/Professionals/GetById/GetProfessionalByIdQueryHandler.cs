using ClinexSync.Application.Data;
using ClinexSync.Contracts.Areas;
using ClinexSync.Contracts.Persons;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Professionals;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Features.Professionals.GetById;

public class GetProfessionalByIdQueryHandler
    : IRequestHandler<GetProfessionalByIdQuery, Result<ProfessionalResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProfessionalByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProfessionalResponse>> Handle(
        GetProfessionalByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        ProfessionalResponse? professional = await _context
            .Professionals.AsNoTracking()
            .Include(p => p.Person)
            .Where(p => p.Id == request.ProfessionalId)
            .Select(professional => new ProfessionalResponse
            {
                Id = professional.Id,
                IdentityId = professional.IdentityId,
                Person = new PersonResponse
                {
                    FirstName = professional.Person.FirstName.Value,
                    LastName = professional.Person.LastName.Value,
                    Phone = professional.Person.Phone.Value,
                    DocumentNumber = professional.Person.FirstName.Value,
                    Email = professional.Person.Email.Value,
                    BirthDay = professional.Person.BirthDay,
                    Genre = professional.Person.Genre.ToString(),
                    CityId = professional.Person.CityId,
                    DistrictId = professional.Person.DistrictId,
                    Address = new AddressResponse
                    {
                        Street1 = professional.Person.Address.Street1,
                        Street2 = professional.Person.Address.Street2,
                        City = professional.Person.Address.City,
                        District = professional.Person.Address.District,
                        PostalCode = professional.Person.Address.PostalCode,
                        IsBis = professional.Person.Address.IsBis,
                        DoorNumber = professional.Person.Address.DoorNumber,
                    },
                },
                Areas = professional.AreasToWork.Join(
                    _context.Areas,
                    atw => atw.Value,
                    a => a.Id,
                    (atw, a) => new AreaResponse { Id = a.Id, Name = a.Name }
                ),
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (professional is null)
        {
            return Result.Failure<ProfessionalResponse>(
                ProfessionalErrors.NotFound(request.ProfessionalId)
            );
        }

        return Result.Success(professional);
    }
}
