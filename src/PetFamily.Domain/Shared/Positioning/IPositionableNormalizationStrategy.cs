namespace PetFamily.Domain.Shared.Positioning;

public interface IPositionableNormalizationStrategy
{
    void Normalize<T>(List<T> positionables, T positionable, int position)
        where T : IPositionable<T>;
}
