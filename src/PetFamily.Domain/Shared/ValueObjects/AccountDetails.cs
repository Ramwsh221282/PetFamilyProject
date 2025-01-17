using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record AccountDetails
{
    private const int MaxAccountDetailsDescriptionLength = 500;
    private const int MaxAccountDetailsNameLength = 100;

    public static AccountDetails Unknown => new AccountDetails("", "");

    public string Description { get; }
    public string Name { get; }

    private AccountDetails(string description, string name)
    {
        Description = description;
        Name = name;
    }

    public static Result<AccountDetails> Create(string? description, string? name)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<AccountDetails>(AccountDetailsErrors.DescriptionIsEmpty());
        if (description.Length > MaxAccountDetailsDescriptionLength)
            return Result.Failure<AccountDetails>(
                AccountDetailsErrors.DescriptionExceedsMaxLength(MaxAccountDetailsDescriptionLength)
            );
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<AccountDetails>(AccountDetailsErrors.NameIsEmpty());
        if (name.Length > MaxAccountDetailsNameLength)
            return Result.Failure<AccountDetails>(
                AccountDetailsErrors.NameExceedsLength(MaxAccountDetailsNameLength)
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
