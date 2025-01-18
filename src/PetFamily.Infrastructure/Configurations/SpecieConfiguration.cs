using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.IdUtils.Implementations;

namespace PetFamily.Infrastructure.Configurations;

internal sealed class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        #region Main

        builder.ToTable("species");
        builder.HasKey(s => s.Id);
        builder
            .Property(s => s.Id)
            .HasConversion(
                toDb => toDb.Id,
                fromDb => new SpecieId(new AdjustedGuidGenerationStrategy(fromDb))
            )
            .HasColumnName("specie_id");

        #endregion

        #region Relations

        builder
            .HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpecieId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Properties with custom conversion

        builder
            .Property(s => s.Type)
            .IsRequired()
            .HasMaxLength(SpecieType.MaxSpecieTypeLength)
            .HasConversion(toDb => toDb.Type, fromDb => SpecieType.Create(fromDb).Value)
            .HasColumnName("specie_type");

        #endregion
    }
}
