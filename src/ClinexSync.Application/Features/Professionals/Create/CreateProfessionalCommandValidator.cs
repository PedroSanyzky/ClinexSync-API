using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Shared;
using FluentValidation;

namespace ClinexSync.Application.Features.Professionals.Create;

public class CreateProfessionalCommandValidator : AbstractValidator<CreateProfessionalCommand>
{
    public CreateProfessionalCommandValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty()
            .WithMessage(PersonErrors.FirstNameEmpty().Description)
            .WithErrorCode(PersonErrors.FirstNameEmpty().Code);

        RuleFor(p => p.LastName)
            .NotEmpty()
            .WithMessage(PersonErrors.LastNameEmpty().Description)
            .WithErrorCode(PersonErrors.LastNameEmpty().Code);

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(PersonErrors.EmailEmpty().Description)
            .WithErrorCode(PersonErrors.EmailEmpty().Code);

        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage(PersonErrors.EmailInvalidFormat().Description)
            .WithErrorCode(PersonErrors.EmailInvalidFormat().Code);

        RuleFor(p => p.documentNumber)
            .NotEmpty()
            .WithMessage(PersonErrors.DocumentNumberEmpty().Description)
            .WithErrorCode(PersonErrors.DocumentNumberEmpty().Code);

        RuleFor(p => p.phone)
            .NotEmpty()
            .WithMessage(PersonErrors.PhoneEmpty().Description)
            .WithErrorCode(PersonErrors.PhoneEmpty().Code);

        RuleFor(p => p.BirthDay)
            .NotEmpty()
            .WithMessage(PersonErrors.BirthDayEmpty().Description)
            .WithErrorCode(PersonErrors.BirthDayEmpty().Code);

        RuleFor(p => p.Genre)
            .NotEmpty()
            .WithMessage(PersonErrors.GenreEmpty().Description)
            .WithErrorCode(PersonErrors.GenreEmpty().Code)
            .Must(genre => Enum.IsDefined(typeof(Genre), genre ?? ""))
            .WithMessage(PersonErrors.InvalidGenre().Description)
            .WithErrorCode(PersonErrors.InvalidGenre().Code);

        RuleFor(p => p.cityId).NotEmpty();
        RuleFor(p => p.districtId).NotEmpty();
    }
}
