using PetFamily.Domain.Shared.SocialMedia.ValueObjects;

namespace PetFamily.Domain.Shared.SocialMedia;

public sealed class SocialMediaCollection
{
    private readonly List<SocialMedia> _socialMedias = [];
    public IReadOnlyCollection<SocialMedia> SocialMedias => _socialMedias;

    public SocialMediaCollection(params SocialMedia[] medias)
    {
        foreach (var media in medias)
        {
            _socialMedias.Add(media);
        }
    }
}

public sealed record SocialMedia
{
    public SocialMediaName Name { get; }
    public SocialMediaUrl Url { get; }

    public SocialMedia(SocialMediaName name, SocialMediaUrl url)
    {
        Name = name;
        Url = url;
    }
}
