using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Contracts.API;

public class PaginatedResponse<T> : ApiResponse<IEnumerable<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public PaginatedResponse(
        string title,
        int status,
        string detail,
        IEnumerable<T>? data,
        int pageNumber,
        int pageSize,
        int totalPages,
        int totalCount,
        Error[]? errors
    )
        : base(title, status, detail, data, errors)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = totalCount;
    }

    public static PaginatedResponse<T> Success(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize,
        int totalCount,
        string title = "Operación exitosa",
        string detail = "La operación se realizó correctamente."
    )
    {
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PaginatedResponse<T>(
            title,
            200,
            detail,
            data,
            pageNumber,
            pageSize,
            totalPages,
            totalCount,
            []
        );
    }

    public static new PaginatedResponse<T> Error(
        string title,
        int status,
        string detail,
        Error[] errors
    )
    {
        return new PaginatedResponse<T>(title, status, detail, default, 0, 0, 0, 0, errors);
    }
}
