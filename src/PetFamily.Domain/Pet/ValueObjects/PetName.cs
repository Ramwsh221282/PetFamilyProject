using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetName
{
    public const int MaxNameLength = 50;

    public string Value { get; }

    private PetName(string value) => Value = value.CapitalizeFirstLetter();

    public static Result<PetName> Create(string? name) =>
        name switch
        {
            null => new Error(PetNameErrors.NameNullError(), ErrorStatusCode.BadRequest),
            not null when string.IsNullOrWhiteSpace(name) => new Error(
                PetNameErrors.NameEmptyError(),
                ErrorStatusCode.BadRequest
            ),
            not null when name.Length > MaxNameLength => new Error(
                PetNameErrors.PetNameExceedsMaximumLengthError(MaxNameLength),
                ErrorStatusCode.BadRequest
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
