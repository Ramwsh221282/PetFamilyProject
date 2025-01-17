using CSharpFunctionalExtensions;
using PetFamily.Domain.Species.ValueObjects;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetColor
{
    private const int MaxColorValueLength = 20;

    public string ColorValue { get; }

    private PetColor(string colorValue) => ColorValue = colorValue;

    public static Result<PetColor> Create(string? colorValue) =>
        colorValue switch
        {
            null => Result.Failure<PetColor>(PetColorErrors.PetColorWasNull()),
            not null when string.IsNullOrWhiteSpace(colorValue) => Result.Failure<PetColor>(
                SpecieTypeErrors.SpecieTypeEmptyError()
            ),
            not null when colorValue.Length > MaxColorValueLength => Result.Failure<PetColor>(
                SpecieTypeErrors.SpecieTypeExceedsLength(MaxColorValueLength)
            ),
            _ => new PetColor(colorValue),
        };
}

public static class PetColorErrors
{
    public static string PetColorWasNull() => "Pet color color was null";

    public static string PetColorWasEmpty() => "Pet color color was empty";

    public static string PetColorExceedsLength(int length) =>
        $"Pet color is more than {length} characters";
}
