using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.SocialMedia.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs;

public record SocialMediaDTO(string Name, string Url);

public static class SocialMediaDTOExtensions
{
    public static SocialMedia ToValueObject(this SocialMediaDTO dto)
    {
        SocialMediaName name = SocialMediaName.Create(dto.Name);
        SocialMediaUrl url = SocialMediaUrl.Create(dto.Url);
        return new SocialMedia(name, url);
    }

    public static SocialMedia[] ToValueObject(this List<SocialMediaDTO>? dto) =>
        dto is null ? [] : dto.Select(d => d.ToValueObject()).ToArray();
}
