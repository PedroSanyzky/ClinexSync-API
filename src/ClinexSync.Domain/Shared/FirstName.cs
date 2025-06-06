﻿using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public record FirstName
{
    public string Value { get; }

    private FirstName(string value) => Value = value;

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<FirstName>(PersonErrors.FirstNameEmpty());
        }

        if (value.Length > 50)
        {
            return Result.Failure<FirstName>(PersonErrors.FirstNameTooLong());
        }

        return Result.Success(new FirstName(value));
    }
}
