using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Infrastructure.Configurations;

internal sealed class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        #region Main

        builder.ToTable("breeds");
        builder.HasKey(b => b.Id);
        builder
            .Property(b => b.Id)
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new BreedId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("breed_id");

        #endregion

        #region Custom conversion properties

        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(BreedName.MaxBreedValueLength)
            .HasColumnName("breed_name")
            .HasConversion(toDb => toDb.BreedValue, fromDb => BreedName.Create(fromDb).Value);

        #endregion
    }
}
