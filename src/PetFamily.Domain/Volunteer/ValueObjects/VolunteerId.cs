using PetFamily.Domain.Utils.IdUtils;

namespace PetFamily.Domain.Volunteer.ValueObjects;

public record VolunteerId
{
    public Guid Id { get; }

    public VolunteerId(IGuidGenerationStrategy strategy) => Id = strategy.Generate();
}
