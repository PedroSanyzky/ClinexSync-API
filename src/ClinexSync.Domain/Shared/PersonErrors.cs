using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public static class PersonErrors
{
    public static Error FirstNameEmpty() =>
        Error.Validation("Person.InvalidFirstName", "First name cannot be empty.");

    public static Error FirstNameTooLong() =>
        Error.Validation(
            "Person.InvalidFirstName",
            "First name is too long. Maximum 50 characters allowed."
        );

    public static Error LastNameEmpty() =>
        Error.Validation("Person.InvalidLastName", "Last name cannot be empty.");

    public static Error LastNameTooLong() =>
        Error.Validation(
            "Person.InvalidLastName",
            "Last name is too long. Maximum 50 characters allowed."
        );

    public static Error BirthDayEmpty() =>
        Error.Validation("Person.InvalidBirthDay", "Birthday cannot be empty.");

    public static Error GenreEmpty() =>
        Error.Validation("Person.InvalidGenre", "Genre cannot be empty.");

    public static Error InvalidGenre() =>
        Error.Validation("Person.InvalidGenre", "Genre is invalid.");

    public static Error PhoneEmpty() =>
        Error.Validation("Person.InvalidPhone", "Phone number cannot be empty.");

    public static Error PhoneInvalidFormat() =>
        Error.Validation("Person.InvalidPhone", "Invalid phone number format.");

    public static Error DocumentNumberEmpty() =>
        Error.Validation("Person.InvalidDocumentNumber", "Document number cannot be empty.");

    public static Error DocumentNumberInvalidFormat() =>
        Error.Validation(
            "Person.InvalidDocumentNumber",
            "Invalid Uruguayan CI format. Expected format: x.xxx.xxx-x or xxxxxxx-x."
        );

    public static Error EmailEmpty() =>
        Error.Validation("Person.InvalidEmail", "Email cannot be empty xd.");

    public static Error EmailInvalidFormat() =>
        Error.Validation("Person.InvalidEmail", "Invalid email format.");

    public static Error EmailAlreadyExists() =>
        Error.Validation("Person.InvalidEmail", "Email already exists.");

    public static Error PhoneAlreadyExists() =>
        Error.Validation("Person.InvalidPhone", "Phone already exists.");

    public static Error DocumentNumberAlreadyExists() =>
        Error.Validation("Person.InvalidDocumentNumber", "Document number already exists.");
}
