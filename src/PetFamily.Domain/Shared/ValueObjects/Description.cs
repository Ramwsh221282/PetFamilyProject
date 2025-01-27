using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Description
{
    public const int MaxDescriptionLength = 500;
    public string Text { get; }
    public static Description Empty = new Description("");
    private Description(string text) => Text = text.CapitalizeFirstLetter();

    public static Result<Description> Create(string? description) => new ResultPipe()
        .Check(!string.IsNullOrWhiteSpace(description) && description.Length > MaxDescriptionLength,
            DescriptionErrors.DescriptionExceedsMaxLength)
        .FromPipe(() => string.IsNullOrWhiteSpace(description) ? Empty : new Description(description));
}

public static class DescriptionErrors
{
    public static Error DescriptionExceedsMaxLength =>
        new Error($"Description can't be more than {Description.MaxDescriptionLength} characters", ErrorStatusCode.BadRequest);
}
