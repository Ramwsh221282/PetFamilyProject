namespace PetFamily.Domain.Shared.Positioning;

public interface IPositionable<T>
{
    Position Position { get; }

    void ChangePosition(Position position);
}
