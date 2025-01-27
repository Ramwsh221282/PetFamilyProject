using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public sealed record PetBirthday
{
    public DateOnly Value { get; }
    private PetBirthday(DateOnly date) => Value = date;
    public static Result<PetBirthday> Create(DateOnly birthday)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        return new ResultPipe()
            .Check(birthday == default, PetBirthdayErrors.ShouldNotBeInvalidDate)
            .Check(birthday > today, PetBirthdayErrors.ShouldNotMoreCurrentDate)
            .FromPipe(new PetBirthday(birthday));
    }
}

public static class PetBirthdayErrors
{
    public static Error ShouldNotMoreCurrentDate => new Error("Pet birthday cannot have more than current date", ErrorStatusCode.BadRequest);
    public static Error ShouldNotBeInvalidDate => new Error("Pet birthday cannot have invalid date", ErrorStatusCode.BadRequest);
}
