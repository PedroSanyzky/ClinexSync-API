using System.Security.Claims;
using ClinexSync.Application.Services.Users;
using ClinexSync.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ClinexSync.Infrastructure.Authorization;

internal sealed class RoleClaimTransformation : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvider;

    public RoleClaimTransformation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (
            principal.Identity is not { IsAuthenticated: true }
            || principal.HasClaim(claim => claim.Type == ClaimTypes.Role)
                && principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub)
        )
        {
            return principal;
        }

        using IServiceScope scope = _serviceProvider.CreateScope();

        IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        string identityId = principal.GetIdentityId();

        Role? userRole = await userService.GetUserRoleAsync(identityId);

        if (userRole is null)
            return principal;

        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userRole.Name));

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
