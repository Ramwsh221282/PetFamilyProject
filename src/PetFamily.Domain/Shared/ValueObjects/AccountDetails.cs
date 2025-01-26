using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public record AccountDetails
{
    public const int MaxAccountDetailsDescriptionLength = 500;
    public const int MaxAccountDetailsNameLength = 100;

    public static AccountDetails Unknown => new AccountDetails("", "");

    public string Description { get; }
    public string Name { get; }

    private AccountDetails(string description, string name)
    {
        Description = description.CapitalizeFirstLetter();
        Name = name.CapitalizeFirstLetter();
    }

    public static Result<AccountDetails> Create(string? description, string? name)
    {
        if (string.IsNullOrWhiteSpace(description))
            return new Error(AccountDetailsErrors.DescriptionIsEmpty(), ErrorStatusCode.BadRequest);
        if (description.Length > MaxAccountDetailsDescriptionLength)
            return new Error(
                AccountDetailsErrors.DescriptionExceedsMaxLength(
                    MaxAccountDetailsDescriptionLength
                ),
                ErrorStatusCode.BadRequest
            );
        if (string.IsNullOrWhiteSpace(name))
            return new Error(AccountDetailsErrors.NameIsEmpty(), ErrorStatusCode.BadRequest);
        if (name.Length > MaxAccountDetailsNameLength)
            return new Error(
                AccountDetailsErrors.NameExceedsLength(MaxAccountDetailsNameLength),
                ErrorStatusCode.BadRequest
            );
        return new AccountDetails(description, name);
    }
}

public static class AccountDetailsErrors
{
    public static string DescriptionIsEmpty() => "Account description was empty";

    public static string DescriptionExceedsMaxLength(int length) =>
        $"Account description is more than {length} characters";

    public static string NameIsEmpty() => "Account name is empty";

    public static string NameExceedsLength(int length) =>
        $"Account name is more than {length} characters";
}
