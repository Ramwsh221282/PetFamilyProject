using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Volunteer.ValueObjects;

public record ExperienceInYears
{
    public int Years { get; }
    private ExperienceInYears(int years) => Years = years;
    public static ExperienceInYears NoExperience = new(0);
    public static Result<ExperienceInYears> Create(int years) => new ResultPipe()
        .Check(years < 0, ExperienceInYearsErrors.ExperienceCannotBeNegative)
        .Check(years > 100, ExperienceInYearsErrors.ExperienceCannotBeMoreThanHundred)
        .FromPipe(() => new ExperienceInYears(years));
}

public static class ExperienceInYearsErrors
{
    public static Error ExperienceCannotBeNegative => 
        new("Experience cannot be negative value", ErrorStatusCode.BadRequest);
    public static Error ExperienceCannotBeMoreThanHundred =>
        new("Experience cannot be more than 100 years", ErrorStatusCode.BadRequest);
}
