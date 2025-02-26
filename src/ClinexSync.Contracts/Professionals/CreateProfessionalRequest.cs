using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinexSync.Contracts.Professionals;

public record CreateProfessionalRequest(
    string FirstName,
    string lastName,
    string phone,
    string documentNumber,
    string Email,
    string Street1,
    string Street2,
    string PostalCode,
    bool IsBis,
    int DoorNumber,
    DateOnly BirthDay,
    string Genre,
    Guid cityId,
    Guid districtId,
    Guid[] AreasToWork
) { }
