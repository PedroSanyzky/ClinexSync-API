using FluentValidation;

namespace ClinexSync.Application.Features.Users.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(LoginErrors.InvalidEmail().Description)
            .WithErrorCode(LoginErrors.InvalidEmail().Code);

        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage(LoginErrors.InvalidEmail().Description)
            .WithErrorCode(LoginErrors.InvalidEmail().Code);

        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(LoginErrors.InvalidPassword().Description)
            .WithErrorCode(LoginErrors.InvalidPassword().Code);
    }
}
