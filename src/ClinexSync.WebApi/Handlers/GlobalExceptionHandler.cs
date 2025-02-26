using ClinexSync.Contracts.API;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClinexSync.WebApi.Handlers;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(exception, "Unhandled exception occurred");

        var problemDetails = ApiResponse<object>.Error(
            "Server failure",
            StatusCodes.Status500InternalServerError,
            "Error ocurred.",
            null
        );

        httpContext.Response.StatusCode = problemDetails.Status;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
