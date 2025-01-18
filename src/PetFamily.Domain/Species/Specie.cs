using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species;

public class Specie
{
    #region Attributes

    private readonly List<Breed> _breeds = [];
    public SpecieId Id { get; init; }
    public SpecieType Type { get; private set; }
    public IReadOnlyCollection<Breed> Breeds => _breeds;

    #endregion

    private Specie() { } // ef core

    public Specie(SpecieType type)
    {
        Id = new SpecieId(new RandomGuidGenerationStrategy());
        Type = type;
    }

    #region Behavior

    public void ChangeSpecieType(SpecieType type) => Type = type;

    public Result AddBreed(Breed breed)
    {
        if (OwnsBreed(breed))
            return new Error("Such breed already exists in this specie");
        _breeds.Add(breed);
        return Result.Success();
    }

    public Result RemoveBreed(Breed breed)
    {
        if (!OwnsBreed(breed))
            return new Error("Specie hasn't such breed");
        _breeds.Remove(breed);
        return Result.Success();
    }

    #endregion

    #region Helper utilities

    private bool OwnsBreed(Breed breed) =>
        _breeds.Any(b => b.Id == breed.Id || b.Name == breed.Name);

    #endregion
}
