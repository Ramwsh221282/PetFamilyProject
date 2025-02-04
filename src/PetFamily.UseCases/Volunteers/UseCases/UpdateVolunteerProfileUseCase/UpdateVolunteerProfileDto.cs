using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;

public record UpdateVolunteerProfileDto(
    PersonNameDTO Name,
    ContactsDTO Contacts,
    DescriptionDTO Description,
    ExperienceInYearsDto ExperienceInYears
);
