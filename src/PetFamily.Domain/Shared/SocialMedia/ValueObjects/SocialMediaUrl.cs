using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.SocialMedia.ValueObjects;

public record SocialMediaUrl
{
    public string Url { get; }

    private SocialMediaUrl(string url) => Url = url;

    public static Result<SocialMediaUrl> Create(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return new Error(SocialMediaUrlErrors.UrlEmpty(), ErrorStatusCode.BadRequest);
        if (!UrlValidationHelper.IsUrlValid(url))
            return new Error(SocialMediaUrlErrors.UrlIsInvalid(), ErrorStatusCode.BadRequest);
        return new SocialMediaUrl(url);
    }
}

public static class SocialMediaUrlErrors
{
    public static string UrlEmpty() => "Social media Url was empty";

    public static string UrlIsInvalid() => "Social media Url is invalid";
}
