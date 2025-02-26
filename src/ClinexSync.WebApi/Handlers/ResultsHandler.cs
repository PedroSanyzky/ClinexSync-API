using ClinexSync.Contracts.API;
using ClinexSync.Contracts.Shared;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.WebApi.Handlers;

public static class ResultsHandler
{
    // Overload para resultados sin dato (Result)
    public static IResult CustomResponse(Result result)
    {
        if (result.IsSuccess)
        {
            var response = ApiResponse<object>.Success(data: null, title: "sucess", detail: "ok");
            return Results.Json(response, statusCode: response.Status);
        }
        else
        {
            int statusCode = GetStatusCode(result.Error.Type);
            string title = GetTitle(result.Error);
            string detail = GetDetail(result.Error);
            Error[]? errors = GetErrors(result) ?? [];

            var response = ApiResponse<object>.Error(
                title: title,
                status: statusCode,
                detail: detail,
                errors: errors
            );
            return Results.Json(response, statusCode: statusCode);
        }
    }

    public static IResult CustomResponse<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            var response = ApiResponse<object>.Success(
                data: result.Value,
                title: "sucess",
                detail: "ok"
            );
            return Results.Json(response, statusCode: response.Status);
        }
        else
        {
            int statusCode = GetStatusCode(result.Error.Type);
            string title = GetTitle(result.Error);
            string detail = GetDetail(result.Error);
            Error[]? errors = GetErrors(result) ?? [];

            var response = ApiResponse<T>.Error(
                title: title,
                status: statusCode,
                detail: detail,
                errors: errors
            );
            return Results.Json(response, statusCode: statusCode);
        }
    }

    public static IResult CustomPaginatedResponse<T>(Result<Paginated<T>> result)
    {
        if (result.IsSuccess)
        {
            // Se utiliza la lógica de éxito de PaginatedResponse que ya hereda de ApiResponse
            var response = PaginatedResponse<T>.Success(
                data: result.Value.Data,
                pageNumber: result.Value.PageNumber,
                pageSize: result.Value.PageSize,
                totalCount: result.Value.TotalCount,
                title: "sucess",
                detail: "ok"
            );
            return Results.Json(response, statusCode: response.Status);
        }
        else
        {
            int statusCode = GetStatusCode(result.Error.Type);
            string title = GetTitle(result.Error);
            string detail = GetDetail(result.Error);
            Error[] errors = GetErrors(result) ?? [];

            var response = PaginatedResponse<T>.Error(
                title: title,
                status: statusCode,
                detail: detail,
                errors: errors
            );
            return Results.Json(response, statusCode: statusCode);
        }
    }

    private static string GetTitle(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => error.Code,
            ErrorType.Problem => error.Code,
            ErrorType.NotFound => error.Code,
            ErrorType.Conflict => error.Code,
            _ => "Server failure",
        };

    private static string GetDetail(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => error.Description,
            ErrorType.Problem => error.Description,
            ErrorType.NotFound => error.Description,
            ErrorType.Conflict => error.Description,
            _ => "An unexpected error occurred",
        };

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

    private static Error[]? GetErrors(Result result)
    {
        if (result.Error is not ValidationError validationError)
        {
            return null;
        }

        return validationError.Errors;
    }
}
