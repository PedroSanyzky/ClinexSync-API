using ClinexSync.Application.Data;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Professionals;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Features.Professionals.GetAll;

public class GetAllProfessionalsQueryHandler
    : IRequestHandler<GetAllProfessionalsQuery, Result<Paginated<BasicProfessionalResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProfessionalsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Paginated<BasicProfessionalResponse>>> Handle(
        GetAllProfessionalsQuery request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Professional> query = _context
            .Professionals.Include(p => p.Person)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.FullName))
        {
            query = query.Where(p =>
                $"{p.Person.FirstName.Value.ToLower() + " " + p.Person.LastName.Value.ToLower()}".StartsWith(
                    request.FullName.ToLower()
                )
            );
        }

        if (request.AreaId is not null)
        {
            query = query.Where(p => p.AreasToWork.Any(a => a.Value == request.AreaId));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        IEnumerable<BasicProfessionalResponse> professionals = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new BasicProfessionalResponse
            {
                Id = p.Id,
                FirstName = p.Person.FirstName.Value,
                LastName = p.Person.LastName.Value,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(
            new Paginated<BasicProfessionalResponse>
            {
                Data = professionals,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
            }
        );
    }
}
