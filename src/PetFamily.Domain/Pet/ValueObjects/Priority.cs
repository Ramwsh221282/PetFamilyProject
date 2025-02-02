namespace PetFamily.Domain.Pet.ValueObjects;

public sealed record Priority
{
    public int Value { get; }

    public Priority(int value) => Value = value;
}
