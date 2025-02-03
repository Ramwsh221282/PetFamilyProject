using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.Positioning;

public sealed record Position
{
    public int Value { get; }

    private Position(int value) => Value = value;

    public static Position Default => new Position(0);

    public static Result<Position> Create<T>(List<T> positionables, int value)
        where T : IPositionable<T>
    {
        if (value < 1 || value > positionables.Count)
            return new Error(
                $"Position value must be between 1 and {positionables.Count}",
                ErrorStatusCode.BadRequest
            );

        return new Position(value);
    }

    public static Position CreateNext<T>(List<T> positionables)
        where T : IPositionable<T> => new Position(positionables.Count + 1);
}
