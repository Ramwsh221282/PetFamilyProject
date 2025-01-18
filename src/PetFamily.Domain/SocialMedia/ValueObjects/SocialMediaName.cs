using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.SocialMedia.ValueObjects;

public record SocialMediaName
{
    public const int SocialMediaNameMaxLength = 50;

    public string Name { get; }

    private SocialMediaName(string name) => Name = name.CapitalizeFirstLetter();

    public static Result<SocialMediaName> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new Error(SocialMediaNameErrors.NameIsEmpty());
        if (name.Length > SocialMediaNameMaxLength)
            return new Error(SocialMediaNameErrors.NameExceedsLength(SocialMediaNameMaxLength));
        return new SocialMediaName(name);
    }
}

public static class SocialMediaNameErrors
{
    public static string NameIsEmpty() => "Social media name is empty";

    public static string NameExceedsLength(int length) =>
        $"Social media name exceeds length of {length} characters";
}
