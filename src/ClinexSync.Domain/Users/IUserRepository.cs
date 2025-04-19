using ClinexSync.Domain.Abstractions;

namespace ClinexSync.Domain.Users;

public interface IUserRepository : IRepository<User, string>
{
    Task<Role?> GetUserRoleById(string id);
}
