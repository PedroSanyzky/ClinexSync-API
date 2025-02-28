using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Application.Data;
using ClinexSync.Contracts.Areas;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Professionals;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Features.Areas.GetAll;

public class GetAllAreasQueryHandler
    : IRequestHandler<GetAllAreasQuery, Result<IEnumerable<AreaResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllAreasQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<AreaResponse>>> Handle(
        GetAllAreasQuery request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Area> query = _context.Areas.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(a => a.Name.ToLower().StartsWith(request.Name.ToLower()));
        }

        IEnumerable<AreaResponse> areas = await query
            .Select(a => new AreaResponse { Id = a.Id, Name = a.Name })
            .ToListAsync(cancellationToken);

        return Result.Success(areas);
    }
}
