using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Contracts.API;

public class ApiResponse<T>
{
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public T? Data { get; set; }
    public Error[]? Errors { get; set; }

    public ApiResponse(
        string title,
        int status,
        string detail,
        T? data = default,
        Error[]? errors = null
    )
    {
        Title = title;
        Status = status;
        Detail = detail;
        Data = data;
        Errors = errors;
    }

    public static ApiResponse<T> Success(T? data, string title = "Sucess", string detail = "Ok.")
    {
        return new ApiResponse<T>(title, 200, detail, data);
    }

    public static ApiResponse<T> Error(string title, int status, string detail, Error[]? errors)
    {
        return new ApiResponse<T>(title, status, detail, default, errors);
    }
}
