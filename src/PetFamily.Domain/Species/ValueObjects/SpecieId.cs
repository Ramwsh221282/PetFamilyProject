using PetFamily.Domain.Utils.IdUtils;

namespace PetFamily.Domain.Species.ValueObjects;

public record SpecieId
{
    public Guid Id { get; }

    public SpecieId(IGuidGenerationStrategy strategy) => Id = strategy.Generate();
}
