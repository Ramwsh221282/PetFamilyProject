using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed record PersonName
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

    public static Result<PersonName> Create(string? name, string? surname, string? patronymic) =>
        new ResultPipe()
            .Check(string.IsNullOrWhiteSpace(name), PersonNameErrors.NameWasEmpty)
            .Check(string.IsNullOrWhiteSpace(surname), PersonNameErrors.SurnameWasEmpty)
            .Check(!string.IsNullOrWhiteSpace(name) && name.Length > MaxNamePartsLength, PersonNameErrors.NameExceedsLength)
            .Check(!string.IsNullOrWhiteSpace(surname) && surname.Length > MaxNamePartsLength, PersonNameErrors.SurnameExceedsLength)
            .Check(!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > MaxNamePartsLength,
                PersonNameErrors.PatronymicExceedsLength)
            .FromPipe(() =>
                string.IsNullOrWhiteSpace(patronymic)
                    ? new PersonName(name!, surname!)
                    : new PersonName(name!, surname!, patronymic));
}

public static class PersonNameErrors
{
    public static Error NameExceedsLength =>
        new($"Name cannot be more than {PersonName.MaxNamePartsLength} characters", ErrorStatusCode.BadRequest);
    public static Error SurnameExceedsLength =>
        new($"Name cannot be more than {PersonName.MaxNamePartsLength} characters", ErrorStatusCode.BadRequest);
    public static Error PatronymicExceedsLength =>
        new($"Name cannot be more than {PersonName.MaxNamePartsLength} characters", ErrorStatusCode.BadRequest);
    public static Error NameWasEmpty => 
        new("Name was empty", ErrorStatusCode.BadRequest);
    public static Error SurnameWasEmpty => 
        new("Surname was empty", ErrorStatusCode.BadRequest);
}
