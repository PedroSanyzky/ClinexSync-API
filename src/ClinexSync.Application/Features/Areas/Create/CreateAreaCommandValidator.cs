using ClinexSync.Domain.Areas;
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
