using PetFamily.Domain.Shared.Positioning.NormalizationStrategies;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.Positioning;

public static class PositionableExtensions
{
    public static Result Normalize<T>(this List<T> positionables, T positionable, int newPosition)
        where T : IPositionable<T>
    {
        Result<Position> newElementPosition = Position.Create(positionables, newPosition);
        if (newElementPosition.IsFailure)
            return newElementPosition.Error;

        int indexOfPositionable = positionables.FindIndex(p => p.Position == positionable.Position);
        if (indexOfPositionable == -1)
            return new Error(
                "Element that you want to move was not found",
                ErrorStatusCode.BadRequest
            );

        int indexOfExisting = positionables.FindIndex(p => p.Position.Value == newPosition);
        if (indexOfExisting == -1)
            return new Error(
                "Element under that position was not found",
                ErrorStatusCode.BadRequest
            );

        IPositionableNormalizationStrategy strategy =
            indexOfExisting < indexOfPositionable
                ? new MoveDownPositionablesStrategy(indexOfPositionable, indexOfExisting)
                : new MoveUpPositionablesStrategy(indexOfPositionable, indexOfExisting);

        strategy.Normalize(positionables, positionable, newPosition);
        positionable.ChangePosition(newElementPosition.Value);
        positionables.Sort((p1, p2) => p1.Position.Value < p2.Position.Value ? -1 : 1);
        return Result.Success();
    }
}
