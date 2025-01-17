using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetBirthday
{
    public DateOnly Value { get; }

    private PetBirthday(DateOnly date) => Value = date;

    public static Result<PetBirthday> Create(DateOnly birthDay)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        if (birthDay > today)
            return Result.Failure<PetBirthday>(PetBirthdayErrors.DateIsMoreThanCurrentDate());
        return new PetBirthday(birthDay);
    }
}

public static class PetBirthdayErrors
{
    public static string DateIsMoreThanCurrentDate() => "Pet cannot be in the future";
}
