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

    public static Result<AccountDetails> Create(string? description, string? name) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(description), AccountDetailsErrors.DescriptionIsEmpty)
        .Check(string.IsNullOrWhiteSpace(name), AccountDetailsErrors.NameIsEmpty)
        .Check(!string.IsNullOrWhiteSpace(description) && description.Length > MaxAccountDetailsDescriptionLength,
            AccountDetailsErrors.DescriptionExceedsMaxLength)
        .Check(!string.IsNullOrWhiteSpace(name) && name.Length > MaxAccountDetailsNameLength, AccountDetailsErrors.NameExceedsLength)
        .FromPipe(new AccountDetails(description!, name!));
}

public static class AccountDetailsErrors
{
    public static Error DescriptionIsEmpty => 
        new("Account description was empty", ErrorStatusCode.BadRequest);
    public static Error DescriptionExceedsMaxLength =>
        new($"Account description is more than {AccountDetails.MaxAccountDetailsDescriptionLength} characters", ErrorStatusCode.BadRequest);
    public static Error NameIsEmpty => 
        new("Account name is empty", ErrorStatusCode.BadRequest);
    public static Error NameExceedsLength =>
        new($"Account name is more than {AccountDetails.MaxAccountDetailsNameLength} characters", ErrorStatusCode.BadRequest);
}
