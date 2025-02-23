using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Shared;

public static class PersonErrors
{
    public static Error FirstNameEmpty() =>
        Error.Failure("Person.InvalidFirstName", "First name cannot be empty.");

    public static Error FirstNameTooLong() =>
        Error.Failure("Person.InvalidFirstName", "First name is too long. Maximum 50 characters allowed.");

    public static Error LastNameEmpty() =>
        Error.Failure("Person.InvalidLastName", "Last name cannot be empty.");

    public static Error LastNameTooLong() =>
        Error.Failure("Person.InvalidLastName", "Last name is too long. Maximum 50 characters allowed.");

    public static Error PhoneEmpty() =>
        Error.Failure("Person.InvalidPhone", "Phone number cannot be empty.");

    public static Error PhoneInvalidFormat() =>
        Error.Failure("Person.InvalidPhone", "Invalid phone number format.");

    public static Error DocumentNumberEmpty() =>
        Error.Failure("Person.InvalidDocumentNumber", "Document number cannot be empty.");

    public static Error DocumentNumberInvalidFormat() =>
        Error.Failure("Person.InvalidDocumentNumber", "Invalid Uruguayan CI format. Expected format: x.xxx.xxx-x or xxxxxxx-x.");

    public static Error EmailEmpty() =>
        Error.Failure("Person.InvalidEmail", "Email cannot be empty.");

    public static Error EmailInvalidFormat() =>
        Error.Failure("Person.InvalidEmail", "Invalid email format.");
}

