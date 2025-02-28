using ClinexSync.Application.Data;
using ClinexSync.Contracts.Areas;
using ClinexSync.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Features.Professionals.GetAreas;

public class GetProfessionalAreasQueryHandler
    : IRequestHandler<GetProfessionalAreasQuery, Result<IEnumerable<AreaResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetProfessionalAreasQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<AreaResponse>>> Handle(
        GetProfessionalAreasQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<AreaResponse> professionalAreas = await _context
            .Professionals.AsNoTracking()
            .Where(p => p.Id == request.ProfessionalId)
            .SelectMany(p => p.AreasToWork)
            .Join(
                _context.Areas,
                atw => atw.Value,
                a => a.Id,
                (atw, a) => new AreaResponse { Id = a.Id, Name = a.Name }
            )
            .ToListAsync(cancellationToken);

        return Result.Success(professionalAreas);
    }
}
