namespace PetFamily.Domain.SocialMedia.ValueObjects;

public record SocialMediaId
{
    public Guid Id { get; } = Guid.NewGuid();
}
