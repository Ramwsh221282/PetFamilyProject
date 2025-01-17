using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SocialMedia.ValueObjects;

public record SocialMediaName
{
    private const int SocialMediaNameMaxLength = 50;

    public string Name { get; }

    private SocialMediaName(string name) => Name = name;

    public static Result<SocialMediaName> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialMediaName>(SocialMediaNameErrors.NameIsEmpty());
        if (name.Length > SocialMediaNameMaxLength)
            return Result.Failure<SocialMediaName>(
                SocialMediaNameErrors.NameExceedsLength(SocialMediaNameMaxLength)
            );
        return new SocialMediaName(name);
    }
}

public static class SocialMediaNameErrors
{
    public static string NameIsEmpty() => "Social media name is empty";

    public static string NameExceedsLength(int length) =>
        $"Social media name exceeds length of {length} characters";
}
