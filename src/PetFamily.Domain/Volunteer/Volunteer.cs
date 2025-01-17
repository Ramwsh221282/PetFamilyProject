using CSharpFunctionalExtensions;
using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.Domain.Volunteer;

// волонтер
public class Volunteer : Entity
{
    // животные волонтера
    private readonly List<Pet.Pet> _pets = [];

    // id волонтера
    public new VolunteerId Id { get; init; }

    // read-only коллекция животных волонтера
    public IReadOnlyCollection<Pet.Pet> Pets => _pets;

    // контактны
    public Contacts Contacts { get; private set; }

    // описание
    public Description Description { get; private set; } = Description.Empty;

    // ФИО
    public PersonName Name { get; private set; }

    // Опыт работы в годах
    public ExperienceInYears Experience { get; private set; } = ExperienceInYears.NoExperience;

    // реквизиты (опционально)
    public AccountDetails AccountDetails { get; private set; } = AccountDetails.Unknown;

    public Volunteer(
        Contacts contacts,
        PersonName name,
        Description? description = null,
        ExperienceInYears? experience = null
    )
    {
        Id = new VolunteerId();
        Contacts = contacts;
        Name = name;
        if (description != null)
            Description = description;
        if (experience != null)
            Experience = experience;
    }

    public Result<int> GetCountOfPetsByStatus(int statusCode)
    {
        if (!Enum.IsDefined(typeof(PetHelpStatuses), statusCode))
            return Result.Failure<int>(PetHelpStatusErrors.UnknownPetHelpStatus());
        PetHelpStatuses status = (PetHelpStatuses)statusCode;
        return _pets.Count(p => p.HelpStatus.StatusCode == status);
    }

    public Result CarryPet(Pet.Pet pet)
    {
        if (_pets.Any(p => p.Id == pet.Id))
            return Result.Failure("Volunteer charges such pet already");
        _pets.Add(pet);
        return Result.Success();
    }

    public Result DropPet(PetId id)
    {
        if (!_pets.Any(p => p.Id == id))
            return Result.Failure("Volunteer doesn't charge this pet");
        _pets.RemoveAll(p => p.Id == id);
        return Result.Success();
    }

    public Result<Pet.Pet> GetPet(Func<Pet.Pet, bool> predicate)
    {
        Pet.Pet? pet = _pets.FirstOrDefault(predicate);
        if (pet == null)
            return Result.Failure<Pet.Pet>("Volunteer doesn't charge this pet");
        return pet;
    }
}
