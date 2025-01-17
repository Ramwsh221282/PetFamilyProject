using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Description
{
    private const int MaxDescriptionLength = 500;

    public string Text { get; }

    public static Description Empty = new Description("");

    private Description(string text)
    {
        Text = text;
    }

    public static Result<Description> Create(string? description) =>
        description switch
        {
            null => Empty,
            not null when description.Length > MaxDescriptionLength => Result.Failure<Description>(
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
