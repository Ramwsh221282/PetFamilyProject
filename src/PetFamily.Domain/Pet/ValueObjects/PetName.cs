using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetName
{
    public const int MaxNameLength = 50;
    public string Value { get; }
    private PetName(string value) => Value = value.CapitalizeFirstLetter();

    public static Result<PetName> Create(string? name) =>
        new ResultPipe()
            .Check(string.IsNullOrWhiteSpace(name), PetNameErrors.NameEmptyError)
            .Check(!string.IsNullOrWhiteSpace(name) && name.Length > MaxNameLength, PetNameErrors.PetNameExceedsMaximumLengthError)
            .FromPipe(new PetName(name!));
}

public static class PetNameErrors
{
    public static Error NameEmptyError => 
        new Error("Pet name was empty", ErrorStatusCode.BadRequest);
    public static Error PetNameExceedsMaximumLengthError => 
        new Error($"Pet name can't be more {PetName.MaxNameLength} characters", ErrorStatusCode.BadRequest);
}
