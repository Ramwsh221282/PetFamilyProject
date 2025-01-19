using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.OptionalPattern;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.Domain.Volunteer;

public class Volunteer
{
    #region Attributes

    private readonly List<Pet.Pet> _pets = [];
    public readonly SocialMediaCollection SocialMedia = new();
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
        ExperienceInYears? experience = null
    )
    {
        Id = new VolunteerId(new RandomGuidGenerationStrategy());
        Contacts = contacts;
        Name = name;
        if (description != null)
            Description = description;
        if (experience != null)
            Experience = experience;
    }

    #region Behavior

    public Optional<int> GetCountOfPetsByStatus(int statusCode)
    {
        if (!Enum.IsDefined(typeof(PetHelpStatuses), statusCode))
            return Optional<int>.None();
        PetHelpStatuses status = (PetHelpStatuses)statusCode;
        return Optional<int>.Some(_pets.Count(p => p.HelpStatus.StatusCode == status));
    }

    public Result CarryPet(Pet.Pet pet)
    {
        if (OwnsPet(pet))
            return new Error("Volunteer charges such pet already");
        _pets.Add(pet);
        return Result.Success();
    }

    public Result DropPet(PetId id)
    {
        if (!OwnsPet(id))
            return new Error("Volunteer doesn't charge this pet");
        _pets.RemoveAll(p => p.Id == id);
        return Result.Success();
    }

    public Optional<Pet.Pet> GetPet(Func<Pet.Pet, bool> predicate) =>
        Optional<Pet.Pet>.Some(_pets.FirstOrDefault(predicate));

    public void IncremenetVolunteerExperience()
    {
        ExperienceInYears years = ExperienceInYears.Create(Experience.Years);
        Experience = years;
    }

    public void AdjustVolunteerExperience(ExperienceInYears experience) => Experience = experience;

    public void SetNewDescription(Description description) => Description = description;

    public void SetNewAccountDetails(AccountDetails details) => AccountDetails = details;

    #endregion

    #region Helper Utilities

    private bool OwnsPet(Pet.Pet pet) => _pets.Any(p => p.Id == pet.Id);

    private bool OwnsPet(PetId petId) => _pets.Any(p => p.Id == petId);

    #endregion
}
