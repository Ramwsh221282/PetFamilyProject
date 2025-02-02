using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.EntityAbstractions;
using PetFamily.Domain.Shared.Positioning;
using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.OptionalPattern;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.Domain.Volunteer;

public sealed class Volunteer : ISoftDeletable, IPositioner<Pet.Pet>
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
    public bool IsDeleted { get; private set; }

    public DateOnly? DeletedOn { get; private set; }

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

    public void Delete()
    {
        if (IsDeleted)
            return;
        IsDeleted = true;
        DeletedOn = DateOnly.FromDateTime(DateTime.Now);
        foreach (Pet.Pet pet in _pets)
            pet.Delete();
    }

    public void Restore()
    {
        if (!IsDeleted)
            return;
        IsDeleted = false;
        DeletedOn = null;
        foreach (Pet.Pet pet in _pets)
            pet.Restore();
    }

    public Optional<int> GetCountOfPetsByStatus(int statusCode)
    {
        if (!Enum.IsDefined(typeof(PetHelpStatuses), statusCode))
            return Optional<int>.None();
        PetHelpStatuses status = (PetHelpStatuses)statusCode;
        return Optional<int>.Some(_pets.Count(p => p.HelpStatus.StatusCode == status));
    }

    public Result<Pet.Pet> CarryPet(
        PetName name,
        SpecieId specieId,
        BreedId breedId,
        PetBodyMetrics bodyMetrics,
        PetColor color,
        PetBirthday birthday,
        PetAddress address,
        Contacts ownerContacts,
        PetHelpStatus helpStatus,
        PetHealthStatus? healthStatus = null,
        Description? description = null
    )
    {
        Pet.Pet pet = new Pet.Pet(
            name,
            specieId,
            breedId,
            bodyMetrics,
            color,
            birthday,
            address,
            ownerContacts,
            helpStatus,
            _pets,
            healthStatus,
            description
        );
        if (OwnsPet(pet))
            return VolunteerErrors.AlreadyCarriesPet(pet);
        _pets.Add(pet);
        return pet;
    }

    public Result Move(Pet.Pet positionable, int newPosition)
    {
        Result moving = _pets.Normalize(positionable, newPosition);
        return moving;
    }

    public Result DropPet(Pet.Pet pet) =>
        new ResultPipe()
            .Check(!OwnsPet(pet), VolunteerErrors.DoesntCarryPet(pet))
            .WithAction(() => _pets.Remove(pet))
            .FromPipe(Result.Success);

    public Optional<Pet.Pet> GetPet(Func<Pet.Pet, bool> predicate) =>
        Optional<Pet.Pet>.Some(_pets.FirstOrDefault(predicate));

    public void UpdateProfile(
        Description? description = null,
        ExperienceInYears? experience = null,
        PersonName? name = null,
        Contacts? contacts = null
    )
    {
        if (description != null)
            Description = description;
        if (experience != null)
            Experience = experience;
        if (name != null)
            Name = name;
        if (contacts != null)
            Contacts = contacts;
    }

    public void CleanUpdateSocialMedia(IEnumerable<SocialMedia> media) =>
        SocialMedia = new SocialMediaCollection(media.ToArray());

    public void CleanUpdateAccountDetails(AccountDetails details) => AccountDetails = details;

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

    public static Error NotFoundWithId(VolunteerId id) =>
        new("Volunteer with {id} does not exist", ErrorStatusCode.Unknown);

    public static Error AlreadyMarkedAsDeleted(VolunteerId id) =>
        new(
            $"Volunteer with identifier {id.Id} is already marked as deleted",
            ErrorStatusCode.BadRequest
        );

    public static Error NotUniqueContacts(Contacts contacts) =>
        new(
            $"Volunteer contacts email: {contacts.Email} and phone: {contacts.Phone} are not unique",
            ErrorStatusCode.BadRequest
        );
}
