using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Description
{
    public const int MaxDescriptionLength = 500;

    public string Text { get; }

    public static Description Empty = new Description("");

    private Description(string text) => Text = text.CapitalizeFirstLetter();

    public static Result<Description> Create(string? description) =>
        description switch
        {
            null => Empty,
            not null when description.Length > MaxDescriptionLength => new Error(
                DescriptionErrors.DescriptionExceedsMaxLength(MaxDescriptionLength)
            ),
            _ => new Description(description),
        };
}

public static class DescriptionErrors
{
    public static string DescriptionExceedsMaxLength(int maxLength) =>
        $"Description can't be more than {maxLength} characters";
}
