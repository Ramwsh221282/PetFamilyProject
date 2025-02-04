using PetFamily.UseCases.Shared.DTOs;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

public sealed record UpdateVolunteerSocialMediaDto(List<SocialMediaDTO> Media);
