namespace ClinexSync.Domain.Users;

public sealed class Role
{
    public static readonly Role Administrator = new(1, nameof(Administrator));
    public static readonly Role Pacient = new(2, nameof(Pacient));
    public static readonly Role Professional = new(3, nameof(Professional));

    public int Id { get; private set; }
    public string Name { get; private set; }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
