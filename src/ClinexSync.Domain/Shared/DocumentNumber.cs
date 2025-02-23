using ClinexSync.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Shared;

public record DocumentNumber
{
    public string Value { get; }

    private DocumentNumber(string value) => Value = value;

    // Acepta ambos formatos:
    // - With dots: x.xxx.xxx-x (ej: 1.234.567-8)
    // - Without dots: xxxxxxx-x (ej: 1234567-8)
    private static readonly Regex UruguayanCIRegex = new Regex(@"^(?:\d\.\d{3}\.\d{3}|\d{7})-\d$", RegexOptions.Compiled);

    public static Result<DocumentNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<DocumentNumber>(PersonErrors.DocumentNumberEmpty());
        if (!UruguayanCIRegex.IsMatch(value))
            return Result.Failure<DocumentNumber>(PersonErrors.DocumentNumberInvalidFormat());

        return Result.Success(new DocumentNumber(value));
    }

}