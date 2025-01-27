using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetAddress
{
    public string Address { get; }
    private PetAddress(string address) => Address = address.CapitalizeFirstLetter();
    
    public static Result<PetAddress> Create(string address) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(address), PetAddressErrors.AddressWasEmpty)
        .FromPipe(new PetAddress(address));
}

public static class PetAddressErrors
{
    public static Error AddressWasEmpty => new Error("Address was empty", ErrorStatusCode.BadRequest);
}
