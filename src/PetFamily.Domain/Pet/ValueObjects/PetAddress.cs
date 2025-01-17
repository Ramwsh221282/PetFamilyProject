using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetAddress
{
    public string Address { get; }

    private PetAddress(string address) => Address = address;

    public static Result<PetAddress> Create(string? address) =>
        address switch
        {
            null => Result.Failure<PetAddress>(PetAddressErrors.AddressIsNull()),
            not null when string.IsNullOrWhiteSpace(address) => Result.Failure<PetAddress>(
                PetAddressErrors.AddressWasEmpty()
            ),
            _ => new PetAddress(address),
        };
}

public static class PetAddressErrors
{
    public static string AddressIsNull() => "Address was null";

    public static string AddressWasEmpty() => "Address was empty";
}
