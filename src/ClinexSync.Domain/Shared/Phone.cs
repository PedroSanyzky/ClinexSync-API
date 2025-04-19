using System.Text.RegularExpressions;
using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public record Phone
{
    public string Value { get; }

    private Phone(string value) => Value = value;

    public static Result<Phone> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Phone>(PersonErrors.PhoneEmpty());

        var phoneRegex = new Regex(@"^[\d\+\-\(\)\s]+$");

        if (!phoneRegex.IsMatch(value))
            return Result.Failure<Phone>(PersonErrors.PhoneInvalidFormat());

        return Result.Success(new Phone(value));
    }
}
