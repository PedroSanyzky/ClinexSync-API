using ClinexSync.Domain.Abstractions;
using ClinexSync.Domain.Areas;
using ClinexSync.Domain.Shared;

namespace ClinexSync.Domain.Professionals;

public sealed class Professional
{
    private readonly List<AreaToWorkId> _areasToWork = [];

    private Professional(Guid id, Person person)
    {
        Id = id;
        Person = person;
        PersonId = person.PersonId;
    }

    private Professional() { }

    public Guid Id { get; private set; }
    public Guid PersonId { get; set; }
    public Person Person { get; private set; }
    public string IdentityId { get; private set; }
    public IReadOnlyList<AreaToWorkId> AreasToWork => _areasToWork.AsReadOnly();

    public static Result<Professional> Create(Person person)
    {
        var professional = new Professional(Guid.NewGuid(), person);

        return Result.Success(professional);
    }

    public Result<AreaToWorkId> AddAreaToWork(Area area)
    {
        var newAreaToWork = new AreaToWorkId(area.Id);

        if (_areasToWork.Any(area => area == newAreaToWork))
        {
            return Result.Failure<AreaToWorkId>(ProfessionalErrors.AreaAlreadyAssigned(area.Name));
        }

        _areasToWork.Add(newAreaToWork);
        return Result.Success(newAreaToWork);
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
