using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Domain.Pet;

public class Pet
{
    #region Attributes

    public PetId Id { get; init; }
    public SpecieId SpecieId { get; }
    public BreedId BreedId { get; }
    public PetCreationDate CreationDate { get; }
    public PetBirthday Birthday { get; }
    public PetName Name { get; private set; }
    public PetBodyMetrics BodyMetrics { get; private set; }
    public PetHealthStatus PetHealth { get; private set; } = new NeitherVaccinatedNorCastrated();
    public Description Description { get; private set; } = Description.Empty;
    public PetHelpStatus HelpStatus { get; private set; }
    public PetAddress Address { get; private set; }
    public Contacts OwnerContacts { get; private set; }
    public PetColor Color { get; private set; }
    public PetAttachments Attachments { get; private set; } = new();

    #endregion

    private Pet() { } // ef core

    public Pet(
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
        Name = name;
        Id = new PetId(new RandomGuidGenerationStrategy());
        SpecieId = specieId;
        BreedId = breedId;
        BodyMetrics = bodyMetrics;
        Birthday = birthday;
        Address = address;
        OwnerContacts = ownerContacts;
        HelpStatus = helpStatus;
        Color = color;
        CreationDate = PetCreationDate.FromDateOnly(DateOnly.FromDateTime(DateTime.Now));
        if (healthStatus != null)
            PetHealth = healthStatus;
        if (description != null)
            Description = description;
    }

    #region Behavior

    public void UpdatePetName(PetName newName) => Name = newName;

    public void UpdatePetBodyMetrics(PetBodyMetrics newBodyMetrics) => BodyMetrics = newBodyMetrics;

    public void UpdatePetHealthStatus(PetHealthStatus newStatus) => PetHealth = newStatus;

    public void UpdatePetDescription(Description newDescription) => Description = newDescription;

    public void UpdatePetHelpStatus(PetHelpStatus newStatus) => HelpStatus = newStatus;

    public void UpdatePetAddress(PetAddress newAddress) => Address = newAddress;

    public void UpdatePetOwnerContacts(Contacts newContacts) => OwnerContacts = newContacts;

    public void UpdatePetColor(PetColor newColor) => Color = newColor;

    #endregion
}
