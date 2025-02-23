using ClinexSync.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Shared;

public record Email
{
    public string Value { get; }
    private Email(string value) => Value = value;

    // Expresión regular básica para validar emails.
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>(PersonErrors.EmailEmpty());
        if (!EmailRegex.IsMatch(value))
            return Result.Failure<Email>(PersonErrors.EmailInvalidFormat());

        return Result.Success(new Email(value));
    }

}