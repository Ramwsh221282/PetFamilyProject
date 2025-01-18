using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.SocialMedia;
using PetFamily.Domain.SocialMedia.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Infrastructure.Configurations;

internal sealed class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {
        #region Main

        builder.ToTable("social_medias");
        builder.HasKey(sm => sm.Id);
        builder
            .Property(sm => sm.Id)
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new SocialMediaId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("social_media_id");

        #endregion

        #region Custom conversion properties

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(SocialMediaName.SocialMediaNameMaxLength)
            .HasColumnName("social_media_name")
            .HasConversion(toDb => toDb.Name, fromDb => SocialMediaName.Create(fromDb).Value);

        builder
            .Property(p => p.Url)
            .IsRequired()
            .HasColumnName("social_media_url")
            .HasConversion(toDb => toDb.Url, fromDb => SocialMediaUrl.Create(fromDb).Value);

        #endregion
    }
}
