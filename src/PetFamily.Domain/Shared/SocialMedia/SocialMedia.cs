using PetFamily.Domain.Shared.SocialMedia.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.SocialMedia;

public sealed class SocialMediaCollection
{
    private readonly List<SocialMedia> _socialMedias = [];
    public IReadOnlyCollection<SocialMedia> SocialMedias => _socialMedias;

    public Result AttachNewSocialMedia(SocialMedia socialMedia)
    {
        if (_socialMedias.Any(sm => sm.Url == socialMedia.Url && sm.Name == socialMedia.Name))
            return new Error("Not unique social media");
        _socialMedias.Add(socialMedia);
        return Result.Success();
    }

    public Result DetachSocialMedia(SocialMedia socialMedia)
    {
        if (!_socialMedias.Any(sm => sm.Url == socialMedia.Url && sm.Name == socialMedia.Name))
            return new Error("Social media was not found");
        _socialMedias.Remove(socialMedia);
        return Result.Success();
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
