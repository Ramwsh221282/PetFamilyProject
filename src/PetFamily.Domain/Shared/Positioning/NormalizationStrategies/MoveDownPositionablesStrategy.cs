namespace PetFamily.Domain.Shared.Positioning.NormalizationStrategies;

public sealed class MoveDownPositionablesStrategy : IPositionableNormalizationStrategy
{
    private readonly int _indexOfPositionable;
    private readonly int _indexOfCurrent;

    public MoveDownPositionablesStrategy(int indexOfPositionable, int indexOfCurrent)
    {
        _indexOfPositionable = indexOfPositionable;
        _indexOfCurrent = indexOfCurrent;
    }

    public void Normalize<T>(List<T> positionables, T positionable, int position)
        where T : IPositionable<T>
    {
        for (int i = _indexOfCurrent; i < _indexOfPositionable; i++)
        {
            IPositionable<T> element = positionables[i];
            Position current = element.Position;
            Position next = Position.Create(positionables, current.Value + 1);
            element.ChangePosition(next);
        }
    }
}
