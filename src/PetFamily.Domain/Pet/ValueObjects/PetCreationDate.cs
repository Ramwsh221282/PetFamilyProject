namespace PetFamily.Domain.Pet.ValueObjects;

public record PetCreationDate
{
    public DateOnly Date { get; } = DateOnly.FromDateTime(DateTime.Now);

    private PetCreationDate(DateOnly dateOnly)
    {
        Date = dateOnly;
    }

    public static PetCreationDate FromDateOnly(DateOnly dateOnly) => new(dateOnly);
}
