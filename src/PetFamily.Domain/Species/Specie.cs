using CSharpFunctionalExtensions;
using PetFamily.Domain.Species.ValueObjects;

namespace PetFamily.Domain.Species;

public class Specie : Entity
{
    private readonly List<Breed> _breeds = [];

    public new SpecieId Id { get; init; }

    public SpecieType Type { get; private set; }

    public IReadOnlyCollection<Breed> Breeds => _breeds;

    public Specie(SpecieType type)
    {
        Id = new SpecieId();
        Type = type;
    }

    public void ChangeSpecieType(SpecieType type) => Type = type;

    public Result AddBreed(Breed breed)
    {
        if (_breeds.Any(b => b.Id == breed.Id || b.Name == breed.Name))
            return Result.Failure("Such breed already exists in this specie");
        _breeds.Add(breed);
        return Result.Success();
    }
}
