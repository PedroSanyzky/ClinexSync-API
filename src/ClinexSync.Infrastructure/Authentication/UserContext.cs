using ClinexSync.Application.Authentication;
using ClinexSync.Infrastructure.Authorization;
using Microsoft.AspNetCore.Http;

namespace ClinexSync.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string IdentityId =>
        _httpContextAccessor.HttpContext?.User.GetIdentityId()
        ?? throw new ApplicationException("User context is unavailable");
}
