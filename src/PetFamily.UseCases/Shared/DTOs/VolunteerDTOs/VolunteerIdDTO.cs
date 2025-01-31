using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;

public sealed record VolunteerIdDTO(string Id);

public static class VolunteerDTOExtensions
{
    public static VolunteerId ToValueObject(this VolunteerIdDTO dto) => 
        new VolunteerId(new AdjustedGuidGenerationStrategy(Guid.Parse(dto.Id)));
}