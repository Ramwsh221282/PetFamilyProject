using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Infrastructure.Configurations;

internal sealed class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        #region Main

        builder.ToTable("pets");
        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new PetId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("pet_id");

        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");
        builder.Property(p => p.DeletedOn).HasColumnName("deleted_on").IsRequired(false);

        #endregion

        #region Complex properties

        builder.ComplexProperty(
            p => p.Position,
            cpb =>
            {
                cpb.Property(prio => prio.Value).HasColumnName("position");
            }
        );

        builder.ComplexProperty(
            p => p.BodyMetrics,
            cpb =>
            {
                cpb.Property(bm => bm.Height).IsRequired().HasColumnName("pet_height");
                cpb.Property(bm => bm.Weight).IsRequired().HasColumnName("pet_weight");
            }
        );

        builder.ComplexProperty(
            p => p.PetHealth,
            cpb =>
            {
                cpb.Property(h => h.IsCastrated).IsRequired().HasColumnName("pet_castrated");
                cpb.Property(h => h.IsVaccinated).IsRequired().HasColumnName("pet_vaccinated");
            }
        );

        builder.ComplexProperty(
            p => p.HelpStatus,
            cpb =>
            {
                cpb.Property(h => h.StatusCode)
                    .IsRequired()
                    .HasColumnName("pet_help_status_code")
                    .HasConversion(toDb => (int)toDb, fromDb => (PetHelpStatuses)fromDb);
                cpb.Property(h => h.StatusText).IsRequired().HasColumnName("pet_status_help");
            }
        );

        builder.ComplexProperty(
            p => p.OwnerContacts,
            cpb =>
            {
                cpb.Property(cont => cont.Email)
                    .IsRequired(false)
                    .HasMaxLength(Contacts.MaxEmailLength)
                    .HasColumnName("owner_email");
                cpb.Property(cont => cont.Phone)
                    .IsRequired()
                    .HasMaxLength(Contacts.MaxPhoneLength)
                    .HasColumnName("owner_phone");
            }
        );

        #endregion

        #region Custom conversion properties

        builder
            .Property(p => p.SpecieId)
            .IsRequired()
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new SpecieId(new AdjustedGuidGenerationStrategy(fromDb))
            );

        builder
            .Property(p => p.BreedId)
            .IsRequired()
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new BreedId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("breed_id");

        builder
            .Property(p => p.CreationDate)
            .IsRequired()
            .HasConversion(toDb => toDb.Date, fromDb => PetCreationDate.FromDateOnly(fromDb))
            .HasColumnName("pet_creation_date");

        builder
            .Property(p => p.Birthday)
            .IsRequired()
            .HasConversion(toDb => toDb.Value, fromDb => PetBirthday.Create(fromDb).Value)
            .HasColumnName("pet_birthday");

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(PetName.MaxNameLength)
            .HasColumnName("pet_name")
            .HasConversion(toDb => toDb.Value, fromDb => PetName.Create(fromDb));

        builder
            .Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(Description.MaxDescriptionLength)
            .HasColumnName("pet_description")
            .HasConversion(toDb => toDb.Text, fromDb => Description.Create(fromDb).Value);

        builder
            .Property(p => p.Address)
            .IsRequired()
            .HasColumnName("pet_address")
            .HasConversion(toDb => toDb.Address, fromDbb => PetAddress.Create(fromDbb).Value);

        builder
            .Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(PetColor.MaxColorValueLength)
            .HasColumnName("pet_color")
            .HasConversion(toDb => toDb.ColorValue, fromDb => PetColor.Create(fromDb));

        #endregion

        #region JSON

        builder.OwnsOne(
            p => p.Attachments,
            onb =>
            {
                onb.ToJson();
                onb.OwnsMany(
                    at => at.Photos,
                    atb =>
                    {
                        atb.Property(photo => photo.Path).IsRequired();
                    }
                );
            }
        );

        #endregion
    }
}
