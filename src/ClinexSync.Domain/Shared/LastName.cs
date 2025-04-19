using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public record LastName
{
    public string Value { get; }

    private LastName(string value) => Value = value;

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<LastName>(PersonErrors.LastNameEmpty());
        if (value.Length > 50)
            return Result.Failure<LastName>(PersonErrors.LastNameTooLong());

        return Result.Success(new LastName(value));
    }
}
