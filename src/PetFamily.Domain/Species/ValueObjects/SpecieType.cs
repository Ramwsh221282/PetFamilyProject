using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species.ValueObjects;

public record SpecieType
{
    public const int MaxSpecieTypeLength = 80;

    public string Type { get; }

    private SpecieType(string type) => Type = type.CapitalizeFirstLetter();

    public static Result<SpecieType> Create(string? type) =>
        type switch
        {
            null => new Error(SpecieTypeErrors.SpecieTypeNullError()),
            not null when string.IsNullOrWhiteSpace(type) => new Error(
                SpecieTypeErrors.SpecieTypeEmptyError()
            ),
            not null when type.Length > MaxSpecieTypeLength => new Error(
                SpecieTypeErrors.SpecieTypeExceedsLength(MaxSpecieTypeLength)
            ),
            _ => new SpecieType(type),
        };
}

public static class SpecieTypeErrors
{
    public static string SpecieTypeNullError() => "Specie type was null";

    public static string SpecieTypeEmptyError() => "Specie type was empty";

    public static string SpecieTypeExceedsLength(int length) =>
        $"Specie type cannot be more than {length} symbols";
}
