using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public record PersonName
{
    public const int MaxNamePartsLength = 50;

    public string Name { get; }

    public string Surname { get; }

    public string Patronymic { get; }

    private PersonName(string name, string surname, string patronymic = "")
    {
        Name = name.CapitalizeFirstLetter();
        Surname = surname.CapitalizeFirstLetter();
        Patronymic = patronymic.CapitalizeFirstLetter();
    }

    public static Result<PersonName> Create(string? name, string? surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new Error(PersonNameErrors.NameWasEmpty());
        if (name.Length > MaxNamePartsLength)
            return new Error(PersonNameErrors.NameExceedsLength(MaxNamePartsLength));
        if (string.IsNullOrWhiteSpace(surname))
            return new Error(PersonNameErrors.SurnameWasEmpty());
        if (surname.Length > MaxNamePartsLength)
            return new Error(PersonNameErrors.SurnameExceedsLength(MaxNamePartsLength));
        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MaxNamePartsLength)
            return new Error(PersonNameErrors.PatronymicExceedsLength(MaxNamePartsLength));
        return string.IsNullOrWhiteSpace(patronymic)
            ? new PersonName(name, surname)
            : new PersonName(name, surname, patronymic);
    }
}

public static class PersonNameErrors
{
    public static string NameExceedsLength(int length) =>
        $"Name cannot be more than {length} characters";

    public static string SurnameExceedsLength(int length) =>
        $"Name cannot be more than {length} characters";

    public static string PatronymicExceedsLength(int length) =>
        $"Name cannot be more than {length} characters";

    public static string NameWasEmpty() => "Name was empty";

    public static string SurnameWasEmpty() => "Surname was empty";
}
