using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species.ValueObjects;

public record SpecieType
{
    private const int MaxSpecieTypeLength = 80;

    public string Type { get; }

    private SpecieType(string type) => Type = type;

    public static Result<SpecieType> Create(string? type) =>
        type switch
        {
            null => Result.Failure<SpecieType>(SpecieTypeErrors.SpecieTypeNullError()),
            not null when string.IsNullOrWhiteSpace(type) => Result.Failure<SpecieType>(
                SpecieTypeErrors.SpecieTypeEmptyError()
            ),
            not null when type.Length > MaxSpecieTypeLength => Result.Failure<SpecieType>(
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
