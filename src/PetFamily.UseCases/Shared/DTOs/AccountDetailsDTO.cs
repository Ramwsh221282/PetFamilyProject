using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs;

public record AccountDetailsDTO(string Name, string Description);

public static class AccountDetailsDTOExtensions
{
    public static AccountDetails ToValueObject(this AccountDetailsDTO dto) =>
        AccountDetails.Create(dto.Name, dto.Description);
}
