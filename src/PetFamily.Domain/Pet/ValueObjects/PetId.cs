using PetFamily.Domain.Utils.IdUtils;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetId
{
    public Guid Id { get; }

    public PetId(IGuidGenerationStrategy strategy) => Id = strategy.Generate();
}
