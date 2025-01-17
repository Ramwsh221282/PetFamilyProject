using CSharpFunctionalExtensions;
using PetFamily.Domain.Utils;

namespace PetFamily.Domain.SocialMedia.ValueObjects;

public record SocialMediaUrl
{
    public string Url { get; }

    private SocialMediaUrl(string url) => Url = url;

    public static Result<SocialMediaUrl> Create(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialMediaUrl>(SocialMediaUrlErrors.UrlEmpty());
        if (!UrlValidationHelper.IsUrlValid(url))
            return Result.Failure<SocialMediaUrl>(SocialMediaUrlErrors.UrlIsInvalid());
        return new SocialMediaUrl(url);
    }
}

public static class SocialMediaUrlErrors
{
    public static string UrlEmpty() => "Social media Url was empty";

    public static string UrlIsInvalid() => "Social media Url is invalid";
}
