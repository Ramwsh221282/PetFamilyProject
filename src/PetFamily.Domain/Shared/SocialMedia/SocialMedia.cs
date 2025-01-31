using PetFamily.Domain.Shared.SocialMedia.ValueObjects;

namespace PetFamily.Domain.Shared.SocialMedia;

public sealed class SocialMediaCollection
{
    private readonly List<SocialMedia> _socialMedias = [];
    public IReadOnlyCollection<SocialMedia> SocialMedias => _socialMedias;
    private SocialMediaCollection() { } // ef core

    public SocialMediaCollection(params SocialMedia[] medias)
    {
        foreach (var media in medias)
        {
            _socialMedias.Add(media);
        }
    }
}

public sealed record SocialMedia(SocialMediaName Name, SocialMediaUrl Url);
