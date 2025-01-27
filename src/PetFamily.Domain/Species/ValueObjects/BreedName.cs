using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species.ValueObjects;

public record BreedName
{
    public const int MaxBreedValueLength = 100;
    public string BreedValue { get; }

    private BreedName(string breed) => BreedValue = breed.CapitalizeFirstLetter();

    public static Result<BreedName> Create(string? breed) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(breed), BreedNameErrors.BreedNameWasEmpty)
        .Check(!string.IsNullOrWhiteSpace(breed) && breed.Length > MaxBreedValueLength, BreedNameErrors.BreedNameValueExceedsMaxLength)
        .FromPipe(() => new BreedName(breed!));
}

public static class BreedNameErrors
{
    public static Error BreedNameWasEmpty => 
        new("Breed was empty", ErrorStatusCode.BadRequest);
    public static Error BreedNameValueExceedsMaxLength =>
        new($"Breed value is more than {BreedName.MaxBreedValueLength} characters", ErrorStatusCode.BadRequest);
}
