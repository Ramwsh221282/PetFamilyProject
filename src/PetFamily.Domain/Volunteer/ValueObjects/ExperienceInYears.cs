using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteer.ValueObjects;

public record ExperienceInYears
{
    public int Years { get; }

    private ExperienceInYears(int years) => Years = years;

    public static ExperienceInYears NoExperience = new ExperienceInYears(0);

    public static Result<ExperienceInYears> Create(int years)
    {
        if (years < 0)
            return Result.Failure<ExperienceInYears>(
                ExperienceInYearsErrors.ExperienceCannotBeNegative()
            );
        if (years >= 100)
            return Result.Failure<ExperienceInYears>(
                ExperienceInYearsErrors.ExperienceCannotBeMoreThanHundred()
            );
        return new ExperienceInYears(100);
    }
}

public static class ExperienceInYearsErrors
{
    public static string ExperienceCannotBeNegative() => "Experience cannot be negative value";

    public static string ExperienceCannotBeMoreThanHundred() =>
        "Experience cannot be more than 100 years";
}
