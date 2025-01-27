using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.SocialMedia.ValueObjects;

public record SocialMediaUrl
{
    public string Url { get; }
    private SocialMediaUrl(string url) => Url = url;

    public static Result<SocialMediaUrl> Create(string? url) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(url), SocialMediaUrlErrors.UrlEmpty)
        .Check(!string.IsNullOrWhiteSpace(url) && !UrlValidationHelper.IsUrlValid(url), SocialMediaUrlErrors.UrlIsInvalid)
        .FromPipe(new SocialMediaUrl(url!));
}

public static class SocialMediaUrlErrors
{
    public static Error UrlEmpty => new Error("Social media Url was empty", ErrorStatusCode.BadRequest);
    public static Error UrlIsInvalid => new Error("Social media Url is invalid", ErrorStatusCode.BadRequest);
}
