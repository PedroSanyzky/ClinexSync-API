using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Shared;
using FluentValidation;

namespace ClinexSync.Application.Features.Professionals.GetById;

public class GetProfessionalByIdQueryValidator : AbstractValidator<GetProfessionalByIdQuery>
{
    public GetProfessionalByIdQueryValidator()
    {
        RuleFor(r => r.ProfessionalId).NotEmpty();
    }
}
