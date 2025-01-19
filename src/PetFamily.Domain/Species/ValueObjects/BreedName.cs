using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species.ValueObjects;

public record BreedName
{
    public const int MaxBreedValueLength = 100;
    public string BreedValue { get; }

    private BreedName(string breed) => BreedValue = breed.CapitalizeFirstLetter();

    public static Result<BreedName> Create(string? breed) =>
        breed switch
        {
            null => new Error(BreedNameErrors.BreedNameWasNull()),
            not null when string.IsNullOrWhiteSpace(breed) => new Error(
                BreedNameErrors.BreedNameWasEmpty()
            ),
            not null when breed.Length > MaxBreedValueLength => new Error(
                BreedNameErrors.BreedNameValueExceedsMaxLength(MaxBreedValueLength)
            ),
            _ => new BreedName(breed),
        };
}

public static class BreedNameErrors
{
    public static string BreedNameWasNull() => "Breed was null";

    public static string BreedNameWasEmpty() => "Breed was empty";

    public static string BreedNameValueExceedsMaxLength(int maxLength) =>
        $"Breed value is more than {maxLength} characters";
}
