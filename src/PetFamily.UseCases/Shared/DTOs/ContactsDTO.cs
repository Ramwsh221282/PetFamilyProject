using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.UseCases.Shared.DTOs;

public record ContactsDTO(string Phone, string? Email);

public static class ContactsDTOExtensions
{
    public static Contacts ToValueObject(this ContactsDTO dto) =>
        Contacts.Create(dto.Phone, dto.Email);
}
