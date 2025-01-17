using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species.ValueObjects;

public record PersonName
{
    private const int MaxNamePartsLength = 50;

    public string Name { get; }

    public string Surname { get; }

    public string Patronymic { get; }

    private PersonName(string name, string surname, string patronymic = "")
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }

    public static Result<PersonName> Create(string? name, string? surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<PersonName>(PersonNameErrors.NameWasEmpty());
        if (name.Length > MaxNamePartsLength)
            return Result.Failure<PersonName>(
                PersonNameErrors.NameExceedsLength(MaxNamePartsLength)
            );
        if (string.IsNullOrWhiteSpace(surname))
            return Result.Failure<PersonName>(PersonNameErrors.SurnameWasEmpty());
        if (surname.Length > MaxNamePartsLength)
            return Result.Failure<PersonName>(
                PersonNameErrors.SurnameExceedsLength(MaxNamePartsLength)
            );
        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MaxNamePartsLength)
            return Result.Failure<PersonName>(
                PersonNameErrors.PatronymicExceedsLength(MaxNamePartsLength)
            );
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
