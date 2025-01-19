namespace PetFamily.Domain.Utils.IdUtils.Implementations;

public sealed class RandomGuidGenerationStrategy : IGuidGenerationStrategy
{
    public Guid Generate() => Guid.NewGuid();
}
