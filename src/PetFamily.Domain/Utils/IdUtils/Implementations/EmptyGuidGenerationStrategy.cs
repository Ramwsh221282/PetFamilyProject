namespace PetFamily.Domain.Utils.IdUtils.Implementations;

public sealed class EmptyGuidGenerationStrategy : IGuidGenerationStrategy
{
    public Guid Generate() => Guid.Empty;
}
