using FluentValidation;

namespace ClinexSync.Application.Features.Professionals.GetById;

public class GetProfessionalByIdQueryValidator : AbstractValidator<GetProfessionalByIdQuery>
{
    public GetProfessionalByIdQueryValidator()
    {
        RuleFor(r => r.ProfessionalId).NotEmpty();
    }
}
