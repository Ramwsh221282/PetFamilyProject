namespace PetFamily.Domain.Species.ValueObjects;

public record SpecieId
{
    public Guid Id { get; } = Guid.NewGuid();
}
