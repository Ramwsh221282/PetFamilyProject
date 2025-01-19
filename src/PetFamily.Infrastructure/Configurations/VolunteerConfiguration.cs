using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared.SocialMedia.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.Infrastructure.Configurations;

internal sealed class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        #region Main

        builder.ToTable("volunteers");
        builder.HasKey(v => v.Id);
        builder
            .Property(v => v.Id)
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new VolunteerId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("volunteer_id");

        #endregion

        #region Relations

        builder
            .HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.SetNull);

        #endregion

        #region Complex Properties

        builder.ComplexProperty(
            v => v.Contacts,
            cpb =>
            {
                cpb.Property(c => c.Email)
                    .IsRequired(false)
                    .HasMaxLength(Contacts.MaxEmailLength)
                    .HasColumnName("volunteer_email");
                cpb.Property(c => c.Phone)
                    .IsRequired()
                    .HasMaxLength(Contacts.MaxPhoneLength)
                    .HasColumnName("volunteer_phone");
            }
        );

        builder.ComplexProperty(
            v => v.Name,
            cpb =>
            {
                cpb.Property(n => n.Name)
                    .IsRequired()
                    .HasMaxLength(PersonName.MaxNamePartsLength)
                    .HasColumnName("volunteer_name");
                cpb.Property(n => n.Surname)
                    .IsRequired()
                    .HasMaxLength(PersonName.MaxNamePartsLength)
                    .HasColumnName("volunteer_surname");
                cpb.Property(n => n.Patronymic)
                    .IsRequired(false)
                    .HasMaxLength(PersonName.MaxNamePartsLength)
                    .HasColumnName("volunteer_patronymic");
            }
        );

        builder.ComplexProperty(
            v => v.AccountDetails,
            cpb =>
            {
                cpb.Property(ad => ad.Description)
                    .IsRequired(false)
                    .HasMaxLength(AccountDetails.MaxAccountDetailsDescriptionLength)
                    .HasColumnName("volunteer_account_details_description");
                cpb.Property(ad => ad.Name)
                    .IsRequired(false)
                    .HasMaxLength(AccountDetails.MaxAccountDetailsNameLength)
                    .HasColumnName("volunteer_account_details_name");
            }
        );

        #endregion

        #region Properties with custom conversion

        builder
            .Property(v => v.Description)
            .IsRequired(false)
            .HasMaxLength(Description.MaxDescriptionLength)
            .HasColumnName("volunteer_description")
            .HasConversion(toDb => toDb.Text, fromDb => Description.Create(fromDb).Value);

        builder
            .Property(v => v.Experience)
            .IsRequired()
            .HasColumnName("volunteer_experience")
            .HasConversion(toDb => toDb.Years, fromDb => ExperienceInYears.Create(fromDb).Value);

        #endregion

        #region JSON

        builder.OwnsOne(
            v => v.SocialMedia,
            onb =>
            {
                onb.ToJson();
                onb.OwnsMany(
                    col => col.SocialMedias,
                    med =>
                    {
                        med.Property(media => media.Name)
                            .HasConversion(
                                toDb => toDb.Name,
                                fromDb => SocialMediaName.Create(fromDb).Value
                            )
                            .IsRequired();
                        med.Property(media => media.Url)
                            .HasConversion(
                                toDb => toDb.Url,
                                fromDb => SocialMediaUrl.Create(fromDb).Value
                            )
                            .IsRequired();
                    }
                );
            }
        );

        #endregion
    }
}
