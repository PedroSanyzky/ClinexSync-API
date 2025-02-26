using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinexSync.Domain.Shared;

public interface IPersonRepository
{
    Task<Person> ExistsPerson(
        string documentNumber,
        string email,
        string phone,
        CancellationToken cancellationToken
    );
}
