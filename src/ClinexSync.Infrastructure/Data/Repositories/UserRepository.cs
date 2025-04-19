using ClinexSync.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ClinexSync.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public Task DeleteAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.IdentityId == id, cancellationToken);
    }

    public async Task<Role?> GetUserRoleById(string id)
    {
        User? user = await dbContext
            .Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.IdentityId == id);

        return user?.Role;
    }

    public Task<IEnumerable<User>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(User entity, CancellationToken cancellationToken)
    {
        dbContext.Attach(entity.Role);
        await dbContext.Users.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
