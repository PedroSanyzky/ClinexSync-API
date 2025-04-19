using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Users;

public sealed class User
{
    public string IdentityId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public Role Role { get; private set; }
    public int RoleId { get; private set; }

    private User() { }

    private User(string identityId, string fullName, string email, Role role)
    {
        IdentityId = identityId;
        FullName = fullName;
        Email = email;
        Role = role;
        RoleId = role.Id;
    }

    public static User Create(string identityId, Role role, Person person)
    {
        var user = new User(
            identityId,
            person.FirstName.Value + " " + person.LastName.Value,
            person.Email.Value,
            role
        );

        return user;
    }
}
