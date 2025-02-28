using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Shared;
using FluentValidation;

namespace ClinexSync.Application.Features.Areas.Create;

public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
{
    public CreateAreaCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(AreaErrors.NameEmpty().Description)
            .WithErrorCode(AreaErrors.NameEmpty().Description);
    }
}
