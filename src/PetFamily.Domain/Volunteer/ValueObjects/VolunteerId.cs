namespace PetFamily.Domain.Volunteer.ValueObjects;

public record VolunteerId
{
    public Guid Id { get; } = Guid.NewGuid();
}
