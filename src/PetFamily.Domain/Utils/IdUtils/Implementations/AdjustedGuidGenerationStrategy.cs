namespace PetFamily.Domain.Utils.IdUtils.Implementations;

public class AdjustedGuidGenerationStrategy : IGuidGenerationStrategy
{
    private readonly Guid _id;

    public AdjustedGuidGenerationStrategy(Guid id) => _id = id;

    public Guid Generate() => _id;
}
