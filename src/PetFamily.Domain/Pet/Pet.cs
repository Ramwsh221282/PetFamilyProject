using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.EntityAbstractions;
using PetFamily.Domain.Shared.Positioning;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Domain.Pet;

public sealed class Pet : ISoftDeletable, IPositionable<Pet>
{
    #region Attributes
    public PetId Id { get; init; }
    public SpecieId SpecieId { get; }
    public BreedId BreedId { get; }
    public PetCreationDate CreationDate { get; }
    public PetBirthday Birthday { get; }
    public PetName Name { get; private set; }
    public PetBodyMetrics BodyMetrics { get; private set; }
    public PetHealthStatus PetHealth { get; private set; } = new PetHealthStatus(false, false);
    public Description Description { get; private set; } = Description.Empty;
    public PetHelpStatus HelpStatus { get; private set; }
    public PetAddress Address { get; private set; }
    public Contacts OwnerContacts { get; private set; }
    public PetColor Color { get; private set; }
    public PetAttachments Attachments { get; private set; } = new();
    public bool IsDeleted { get; private set; }
    public DateOnly? DeletedOn { get; private set; }
    public Position Position { get; private set; }

    #endregion

    private Pet() { } // ef core

    // Только волонтёр может добавить животное. Это доменная логика, поэтому конструктор internal.
    internal Pet(
        PetName name,
        SpecieId specieId,
        BreedId breedId,
        PetBodyMetrics bodyMetrics,
        PetColor color,
        PetBirthday birthday,
        PetAddress address,
        Contacts ownerContacts,
        PetHelpStatus helpStatus,
        Position position, // вместо передачи списка петов, просто позиция, которая назначается в CarryPet() у волонтёра.
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
        Position = position;
    }

    #region Behavior

    public void Update(
        PetName? name = null,
        PetBodyMetrics? body = null,
        PetHealthStatus? health = null,
        Description? description = null,
        PetHelpStatus? help = null,
        PetAddress? address = null,
        Contacts? ownerContacts = null,
        PetColor? color = null
    )
    {
        if (name != null)
            Name = name;

        if (body != null)
            BodyMetrics = body;

        if (health != null)
            PetHealth = health;

        if (description != null)
            Description = description;

        if (help != null)
            HelpStatus = help;

        if (address != null)
            Address = address;

        if (ownerContacts != null)
            OwnerContacts = ownerContacts;

        if (color != null)
            Color = color;
    }

    public void ChangePosition(Position position) => Position = position;

    public void Delete()
    {
        if (IsDeleted)
            return;
        IsDeleted = true;
        DeletedOn = DateOnly.FromDateTime(DateTime.Now);
    }

    public void Restore()
    {
        if (!IsDeleted)
            return;
        IsDeleted = false;
        DeletedOn = null;
    }

    #endregion
}
