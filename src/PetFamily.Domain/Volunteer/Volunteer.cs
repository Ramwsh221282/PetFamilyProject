using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.OptionalPattern;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.Domain.Volunteer;

public sealed class Volunteer
{
    #region Attributes

    private readonly List<Pet.Pet> _pets = [];
    public SocialMediaCollection SocialMedia { get; private set; }
    public VolunteerId Id { get; }
    public IReadOnlyCollection<Pet.Pet> Pets => _pets;
    public Contacts Contacts { get; private set; }
    public PersonName Name { get; private set; }
    public Description Description { get; private set; } = Description.Empty;
    public ExperienceInYears Experience { get; private set; } = ExperienceInYears.NoExperience;
    public AccountDetails AccountDetails { get; private set; } = AccountDetails.Unknown;

    #endregion

    private Volunteer() { } // ef core

    public Volunteer(
        Contacts contacts,
        PersonName name,
        Description? description = null,
        ExperienceInYears? experience = null,
        AccountDetails? details = null,
        params SocialMedia[] media
    )
    {
        Id = new VolunteerId(new RandomGuidGenerationStrategy());
        Contacts = contacts;
        Name = name;
        SocialMedia = new SocialMediaCollection();
        if (description != null)
            Description = description;
        if (experience != null)
            Experience = experience;
        if (details != null)
            AccountDetails = details;
        SocialMedia = new SocialMediaCollection(media);
    }

    #region Behavior

    public Optional<int> GetCountOfPetsByStatus(int statusCode)
    {
        if (!Enum.IsDefined(typeof(PetHelpStatuses), statusCode))
            return Optional<int>.None();
        PetHelpStatuses status = (PetHelpStatuses)statusCode;
        return Optional<int>.Some(_pets.Count(p => p.HelpStatus.StatusCode == status));
    }

    public Result CarryPet(Pet.Pet pet) =>
        new ResultPipe()
            .Check(!OwnsPet(pet), VolunteerErrors.AlreadyCarriesPet(pet))
            .WithAction(() => _pets.Add(pet))
            .FromPipe(Result.Success);

    public Result DropPet(Pet.Pet pet) =>
        new ResultPipe()
            .Check(!OwnsPet(pet), VolunteerErrors.DoesntCarryPet(pet))
            .WithAction(() => _pets.Remove(pet))
            .FromPipe(Result.Success);

    public Optional<Pet.Pet> GetPet(Func<Pet.Pet, bool> predicate) =>
        Optional<Pet.Pet>.Some(_pets.FirstOrDefault(predicate));

    public void Update(
        Description? description = null,
        ExperienceInYears? experience = null,
        AccountDetails? details = null
    )
    {
        if (description != null)
            Description = description;
        if (experience != null)
            Experience = experience;
        if (details != null)
            AccountDetails = details;
    }

    public void IncremenetVolunteerExperience()
    {
        int years = Experience.Years;
        years += 1;
        Experience = ExperienceInYears.Create(years);
    }

    #endregion

    #region Helper Utilities

    private bool OwnsPet(Pet.Pet pet) => _pets.Any(p => p.Id == pet.Id);

    #endregion
}

public static class VolunteerErrors
{
    public static Error AlreadyCarriesPet(Pet.Pet pet) =>
        new($"Volunteer already carries {pet.Name}", ErrorStatusCode.BadRequest);

    public static Error DoesntCarryPet(Pet.Pet pet) =>
        new($"Volunteer doesn't carry {pet.Name}", ErrorStatusCode.BadRequest);

    public static Error NotFoundWithId(VolunteerId Id) =>
        new("Volunteer with {id} does not exist", ErrorStatusCode.Unknown);
}
