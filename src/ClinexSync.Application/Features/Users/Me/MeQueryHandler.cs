using ClinexSync.Application.Authentication;
using ClinexSync.Application.Data;
using ClinexSync.Contracts.Authentication;
using ClinexSync.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Application.Features.Users.Me;

internal class MeQueryHandler : IRequestHandler<MeQuery, Result<MeResponse>>
{
    private readonly IUserContext _userContext;
    private readonly IApplicationDbContext _dbContext;

    public MeQueryHandler(IUserContext userContext, IApplicationDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    public async Task<Result<MeResponse>> Handle(
        MeQuery request,
        CancellationToken cancellationToken
    )
    {
        MeResponse? userData = await _dbContext
            .Users.AsNoTracking()
            .Include(u => u.Role)
            .Where(u => u.IdentityId == _userContext.IdentityId)
            .Select(user => new MeResponse
            {
                IdentityId = user.IdentityId,
                Fullname = user.FullName,
                Email = user.Email,
                Role = user.Role.Name,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (userData is null)
        {
            return Result.Failure<MeResponse>(Error.NullValue);
        }

        return Result.Success(userData);
    }
}
