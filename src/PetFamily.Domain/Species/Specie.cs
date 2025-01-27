using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Species;

public class Specie
{
    #region Attributes

    private readonly List<Breed> _breeds = [];
    public SpecieId Id { get; }
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

    public void ChangeType(SpecieType type) => Type = type;

    public Result AddBreed(Breed breed) => new ResultPipe()
        .Check(OwnsBreed(breed), SpecieErrors.SpecieAlreadyHasThisBreed(breed))
        .WithAction(() => _breeds.Add(breed))
        .FromPipe(Result.Success);
    
    public Result RemoveBreed(Breed breed) => new ResultPipe()
        .Check(!OwnsBreed(breed), SpecieErrors.SpecieDoesntHaveThisBreed(breed))
        .WithAction(() => _breeds.Remove(breed))
        .FromPipe(Result.Success);

    #endregion

    #region Helper utilities

    private bool OwnsBreed(Breed breed) =>
        _breeds.Any(b => b.Id == breed.Id || b.Name == breed.Name);

    #endregion
}

public static class SpecieErrors
{
    public static Error SpecieAlreadyHasThisBreed(Breed breed) =>
        new Error($"Specie already has this breed {breed.Name}", ErrorStatusCode.BadRequest);
    public static Error SpecieDoesntHaveThisBreed(Breed breed) =>
        new Error($"Specie doesn't have this breed {breed.Name}", ErrorStatusCode.BadRequest);
}