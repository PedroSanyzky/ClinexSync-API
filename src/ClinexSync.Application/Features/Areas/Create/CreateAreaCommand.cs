using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinexSync.Domain.Abstractions;
using MediatR;

namespace ClinexSync.Application.Features.Areas.Create;

public record CreateAreaCommand(string Name) : IRequest<Result<Guid>> { }
