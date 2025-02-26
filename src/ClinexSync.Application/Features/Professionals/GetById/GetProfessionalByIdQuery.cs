using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Contracts.Professionals;
using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Professionals;
using MediatR;

namespace ClinexSync.Application.Features.Professionals.GetById;

public record GetProfessionalByIdQuery(Guid ProfessionalId)
    : IRequest<Result<ProfessionalResponse>> { }
