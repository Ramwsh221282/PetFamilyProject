using PetFamily.Scratches.Common.ResultPattern;

namespace PetFamily.Scratches.FluentValidationScratches;

public record Contacts
{
    public string Email { get; }
    private Contacts(string email) => Email = email;

    public static Result<Contacts> Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return new Error("Email is required", ErrorCodes.SomeCode);
        return new Contacts(email);
    }
}