namespace PetFamily.Domain.Utils.OptionalPattern;

public sealed class Optional<TValue>
{
    private readonly TValue? _value;

    private Optional(TValue? value) => _value = value;

    public static Optional<TValue> None() => new(default);

    public static Optional<TValue> Some(TValue? value) => new(value);

    public bool HasValue => _value is not null;

    public bool TryGetValue(out TValue value)
    {
        if (HasValue)
        {
            value = _value!;
            return true;
        }

        value = default!;
        return false;
    }
}
