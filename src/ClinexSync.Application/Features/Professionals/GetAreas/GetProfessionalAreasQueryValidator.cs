using FluentValidation;

namespace ClinexSync.Application.Features.Professionals.GetAreas;

public class GetProfessionalAreasQueryValidator : AbstractValidator<GetProfessionalAreasQuery>
{
    public GetProfessionalAreasQueryValidator()
    {
        RuleFor(r => r.ProfessionalId).NotEmpty();
    }
}
