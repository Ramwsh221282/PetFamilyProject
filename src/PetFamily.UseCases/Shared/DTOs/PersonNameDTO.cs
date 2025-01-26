using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs;

public record PersonNameDTO(string Name, string Surname, string? Patronymic);

public static class PersonNameDTOExtensions
{
    public static PersonName ToValueObject(this PersonNameDTO dto) =>
        PersonName.Create(dto.Name, dto.Surname, dto.Patronymic);
}
