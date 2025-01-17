using System.Text;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetName
{
    private const int MaxNameLength = 50;

    public string Value { get; }

    private PetName(string value)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToUpper(value[0]));
        stringBuilder.Append(value.Substring(1));
        Value = stringBuilder.ToString().Trim();
    }

    public static Result<PetName> Create(string? name) =>
        name switch
        {
            null => Result.Failure<PetName>(PetNameErrors.NameNullError()),
            not null when string.IsNullOrWhiteSpace(name) => Result.Failure<PetName>(
                PetNameErrors.NameEmptyError()
            ),
            not null when name.Length > MaxNameLength => Result.Failure<PetName>(
                PetNameErrors.PetNameExceedsMaximumLengthError(MaxNameLength)
            ),
            _ => new PetName(name),
        };
}

public static class PetNameErrors
{
    public static string NameNullError() => "Pet name was null";

    public static string NameEmptyError() => "Pet name was empty";

    public static string PetNameExceedsMaximumLengthError(int maxLength) =>
        $"Pet name can't be more {maxLength} characters";
}
