using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetColor
{
    public const int MaxColorValueLength = 20;

    public string ColorValue { get; }

    private PetColor(string colorValue) => ColorValue = colorValue.CapitalizeFirstLetter();

    public static Result<PetColor> Create(string? colorValue) =>
        colorValue switch
        {
            null => new Error(PetColorErrors.PetColorWasNull(), ErrorStatusCode.BadRequest),
            not null when string.IsNullOrWhiteSpace(colorValue) => new Error(
                SpecieTypeErrors.SpecieTypeEmptyError(),
                ErrorStatusCode.BadRequest
            ),
            not null when colorValue.Length > MaxColorValueLength => new Error(
                SpecieTypeErrors.SpecieTypeExceedsLength(MaxColorValueLength),
                ErrorStatusCode.BadRequest
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
