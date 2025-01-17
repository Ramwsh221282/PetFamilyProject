using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species.ValueObjects;

public record BreedName
{
    private const int MaxBreedValueLength = 100;
    public string BreedValue { get; }

    private BreedName(string breed) => BreedValue = breed;

    public static Result<BreedName> Create(string? breed) =>
        breed switch
        {
            null => Result.Failure<BreedName>(BreedNameErrors.BreedNameWasNull()),
            not null when string.IsNullOrWhiteSpace(breed) => Result.Failure<BreedName>(
                BreedNameErrors.BreedNameWasEmpty()
            ),
            not null when breed.Length > MaxBreedValueLength => Result.Failure<BreedName>(
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
