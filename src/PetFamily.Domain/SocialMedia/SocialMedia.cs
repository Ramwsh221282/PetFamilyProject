using CSharpFunctionalExtensions;
using PetFamily.Domain.SocialMedia.ValueObjects;

namespace PetFamily.Domain.SocialMedia;

public class SocialMedia : Entity
{
    public new SocialMediaId Id { get; init; }
    public SocialMediaName Name { get; init; }
    public SocialMediaUrl Url { get; init; }

    public SocialMedia(SocialMediaName name, SocialMediaUrl url)
    {
        Id = new SocialMediaId();
        Name = name;
        Url = url;
    }
}
