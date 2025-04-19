namespace ClinexSync.Contracts.Persons;

public sealed class PersonResponse
{
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDay { get; set; }
    public string Genre { get; set; }
    public AddressResponse Address { get; set; }
}
