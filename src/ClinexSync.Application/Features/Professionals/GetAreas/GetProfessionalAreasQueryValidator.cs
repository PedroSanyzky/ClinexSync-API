using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ClinexSync.Application.Features.Professionals.GetAreas;

public class GetProfessionalAreasQueryValidator : AbstractValidator<GetProfessionalAreasQuery>
{
    public GetProfessionalAreasQueryValidator()
    {
        RuleFor(r => r.ProfessionalId).NotEmpty();
    }
}
