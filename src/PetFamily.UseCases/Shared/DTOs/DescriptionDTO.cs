using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs;

public record DescriptionDTO(string Text);

public static class DescriptionDTOExtensions
{
    public static Description ToValueObject(this DescriptionDTO dto) =>
        Description.Create(dto.Text);
}
