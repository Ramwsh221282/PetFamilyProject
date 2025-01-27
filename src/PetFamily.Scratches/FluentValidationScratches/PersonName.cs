using PetFamily.Scratches.Common.ResultPattern;

namespace PetFamily.Scratches.FluentValidationScratches;

public sealed record PersonName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Patronymic { get; }

    private PersonName(string firstName, string lastName, string patronymic) =>
        (FirstName, LastName, Patronymic) = (firstName, lastName, patronymic);

    public static Result<PersonName> Create(string? firstName, string? lastName, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return new Error("First name is required", ErrorCodes.SomeCode);
        if (string.IsNullOrWhiteSpace(lastName))
            return new Error("Last name is required", ErrorCodes.SomeCode);
        return new PersonName(firstName, lastName, string.IsNullOrWhiteSpace(patronymic) ? "" : patronymic);
    }
}