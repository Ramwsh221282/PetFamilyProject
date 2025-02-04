using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.Positioning;

public interface IPositioner<T>
    where T : IPositionable<T>
{
    Result Move(T positionable, int newPosition);
}
