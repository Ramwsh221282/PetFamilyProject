namespace PetFamily.Domain.Species.ValueObjects;

public record BreedId
{
    public Guid Id { get; } = Guid.NewGuid();
}
