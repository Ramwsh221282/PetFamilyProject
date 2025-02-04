using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;

public record ExperienceInYearsDto(int Experience);

public static class ExperienceInYearsDtoExtensions
{
    public static ExperienceInYears ToValueObject(this ExperienceInYearsDto dto) =>
        ExperienceInYears.Create(dto.Experience);
}
