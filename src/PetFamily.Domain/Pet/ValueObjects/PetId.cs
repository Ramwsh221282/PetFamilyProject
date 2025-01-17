namespace PetFamily.Domain.Pet.ValueObjects;

public record PetId
{
    public Guid Id { get; } = Guid.NewGuid();
}
