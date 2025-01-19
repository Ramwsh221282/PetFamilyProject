using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Domain.Species;

public class Breed
{
    #region Attributes

    public BreedId Id { get; }
    public SpecieId SpecieId { get; }
    public BreedName Name { get; private set; }

    #endregion

    private Breed() { } // ef core

    public Breed(SpecieId specieId, BreedName name)
    {
        Id = new BreedId(new RandomGuidGenerationStrategy());
        SpecieId = specieId;
        Name = name;
    }

    #region Behavior

    public void ChangeBreedName(BreedName name) => Name = name;

    #endregion
}
