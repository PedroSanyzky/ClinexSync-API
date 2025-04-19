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
