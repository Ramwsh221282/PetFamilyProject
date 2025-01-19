using PetFamily.Domain.Utils.IdUtils;

namespace PetFamily.Domain.Shared.SocialMedia.ValueObjects;

public record SocialMediaId
{
    public Guid Id { get; }

    public SocialMediaId(IGuidGenerationStrategy strategy) => Id = strategy.Generate();
}
