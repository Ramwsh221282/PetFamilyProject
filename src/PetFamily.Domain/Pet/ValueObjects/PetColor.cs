using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetColor
{
    public const int MaxColorValueLength = 20;

    public string ColorValue { get; }

    private PetColor(string colorValue) => ColorValue = colorValue.CapitalizeFirstLetter();

    public static Result<PetColor> Create(string? color) =>
        new ResultPipe()
            .Check(string.IsNullOrWhiteSpace(color), PetColorErrors.PetColorWasEmpty)
            .Check(!string.IsNullOrWhiteSpace(color) && color.Length > MaxColorValueLength, PetColorErrors.PetColorExceedsLength)
            .FromPipe(new PetColor(color!));
}

public static class PetColorErrors
{
    public static Error PetColorWasEmpty => 
        new("Pet color color was empty", ErrorStatusCode.BadRequest);
    public static Error PetColorExceedsLength => 
        new($"Pet color is more than {PetColor.MaxColorValueLength} characters", ErrorStatusCode.BadRequest);
}
