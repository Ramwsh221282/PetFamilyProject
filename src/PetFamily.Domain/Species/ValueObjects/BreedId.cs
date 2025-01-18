using PetFamily.Domain.Utils.IdUtils;

namespace PetFamily.Domain.Species.ValueObjects;

public record BreedId
{
    public Guid Id { get; }

    public BreedId(IGuidGenerationStrategy strategy) => Id = strategy.Generate();
}
