﻿using System.Security.Claims;

namespace ClinexSync.Infrastructure.Authorization;

internal static class ClaimsPrincipalExtensions
{
    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ApplicationException("User identity is unavailable");
    }
}
