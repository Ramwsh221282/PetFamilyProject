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
            null => new Error(SpecieTypeErrors.SpecieTypeNullError(), ErrorStatusCode.BadRequest),
            not null when string.IsNullOrWhiteSpace(type) => new Error(
                SpecieTypeErrors.SpecieTypeEmptyError(),
                ErrorStatusCode.BadRequest
            ),
            not null when type.Length > MaxSpecieTypeLength => new Error(
                SpecieTypeErrors.SpecieTypeExceedsLength(MaxSpecieTypeLength),
                ErrorStatusCode.BadRequest
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
