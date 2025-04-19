namespace ClinexSync.Contracts.Persons;

public sealed class AddressResponse
{
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string PostalCode { get; set; }
    public bool IsBis { get; set; }
    public int DoorNumber { get; set; }
}
