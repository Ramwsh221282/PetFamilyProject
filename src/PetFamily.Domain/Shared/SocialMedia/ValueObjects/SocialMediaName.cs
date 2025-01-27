using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.SocialMedia.ValueObjects;

public record SocialMediaName
{
    public const int SocialMediaNameMaxLength = 50;
    public string Name { get; }

    private SocialMediaName(string name) => Name = name.CapitalizeFirstLetter();

    public static Result<SocialMediaName> Create(string? name) =>
        new ResultPipe()
            .Check(string.IsNullOrWhiteSpace(name), SocialMediaNameErrors.NameIsEmpty)
            .Check(!string.IsNullOrWhiteSpace(name) && name.Length > SocialMediaNameMaxLength, SocialMediaNameErrors.NameExceedsLength)
            .FromPipe(new SocialMediaName(name!));
}

public static class SocialMediaNameErrors
{
    public static Error NameIsEmpty => 
        new Error("Social media name is empty", ErrorStatusCode.BadRequest);

    public static Error NameExceedsLength =>
        new Error($"Social media name exceeds length of {SocialMediaName.SocialMediaNameMaxLength} characters",
            ErrorStatusCode.BadRequest);
}
