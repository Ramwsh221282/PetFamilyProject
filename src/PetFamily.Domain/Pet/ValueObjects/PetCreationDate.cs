namespace PetFamily.Domain.Pet.ValueObjects;

public record PetCreationDate
{
    public DateOnly Date { get; } = DateOnly.FromDateTime(DateTime.Now);
}
