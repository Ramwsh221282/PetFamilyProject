using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetAddress
{
    public string Address { get; }

    private PetAddress(string address) => Address = address.CapitalizeFirstLetter();

    public static Result<PetAddress> Create(string? address) =>
        address switch
        {
            null => new Error(PetAddressErrors.AddressIsNull(), ErrorStatusCode.BadRequest),
            not null when string.IsNullOrWhiteSpace(address) => new Error(
                PetAddressErrors.AddressWasEmpty(),
                ErrorStatusCode.BadRequest
            ),
            _ => new PetAddress(address),
        };
}

public static class PetAddressErrors
{
    public static string AddressIsNull() => "Address was null";

    public static string AddressWasEmpty() => "Address was empty";
}
