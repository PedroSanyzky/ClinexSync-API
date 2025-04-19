using ClinexSync.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class PersonRepository(ApplicationDbContext dbContext) : IPersonRepository
{
    public async Task<Person> ExistsPerson(
        string documentNumber,
        string email,
        string phone,
        CancellationToken cancellationToken
    )
    {
        return await dbContext.Persons.FirstOrDefaultAsync(
            person =>
                person.DocumentNumber.Value == documentNumber
                || person.Email.Value == email
                || person.Phone.Value == phone,
            cancellationToken
        );
    }
}
