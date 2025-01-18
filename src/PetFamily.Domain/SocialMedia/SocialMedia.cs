using PetFamily.Domain.SocialMedia.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Domain.SocialMedia;

public class SocialMedia
{
    public SocialMediaId Id { get; init; }
    public SocialMediaName Name { get; init; }
    public SocialMediaUrl Url { get; init; }

    public SocialMedia(SocialMediaName name, SocialMediaUrl url)
    {
        Id = new SocialMediaId(new RandomGuidGenerationStrategy());
        Name = name;
        Url = url;
    }
}
