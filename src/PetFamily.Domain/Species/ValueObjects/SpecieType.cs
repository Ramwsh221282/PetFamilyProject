using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species.ValueObjects;

public record SpecieType
{
    public const int MaxSpecieTypeLength = 80;
    public string Type { get; }
    private SpecieType(string type) => Type = type.CapitalizeFirstLetter();

    public static Result<SpecieType> Create(string? type) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(type), SpecieTypeErrors.SpecieTypeEmptyError)
        .Check(!string.IsNullOrWhiteSpace(type) && type.Length > MaxSpecieTypeLength, SpecieTypeErrors.SpecieTypeExceedsLength)
        .FromPipe(() => new SpecieType(type!));
}

public static class SpecieTypeErrors
{
    public static Error SpecieTypeEmptyError => 
        new("Specie type was empty", ErrorStatusCode.BadRequest);
    public static Error SpecieTypeExceedsLength =>
        new($"Specie type cannot be more than {SpecieType.MaxSpecieTypeLength} symbols", ErrorStatusCode.BadRequest);
}
