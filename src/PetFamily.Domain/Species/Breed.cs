using CSharpFunctionalExtensions;
using PetFamily.Domain.Species.ValueObjects;

namespace PetFamily.Domain.Species;

public class Breed : Entity
{
    public new BreedId Id { get; init; }
    public BreedName Name { get; private set; }

    public Breed(BreedName name)
    {
        Id = new BreedId();
        Name = name;
    }

    public void ChangeBreedName(BreedName name) => Name = name;
}
