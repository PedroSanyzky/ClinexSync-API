namespace ClinexSync.Domain.Shared;

public record Address(
    string Street1,
    string Street2,
    string City,
    string PostalCode,
    string District,
    bool IsBis,
    int DoorNumber
);
