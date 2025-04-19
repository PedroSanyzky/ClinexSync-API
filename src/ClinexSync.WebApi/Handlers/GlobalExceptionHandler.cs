using ClinexSync.Contracts.API;
using Microsoft.AspNetCore.Diagnostics;

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
        if (exception is BadHttpRequestException jsonEx)
        {
            var problemExceptionDetails = ApiResponse<object>.Error(
                "Request body is invalid.",
                StatusCodes.Status400BadRequest,
                "Please check it.",
                null
            );

            httpContext.Response.StatusCode = problemExceptionDetails.Status;
            await httpContext.Response.WriteAsJsonAsync(problemExceptionDetails, cancellationToken);
            return true;
        }

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
