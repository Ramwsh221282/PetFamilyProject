using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public abstract record Contacts
{
    public const int MaxPhoneLength = 50;
    public const int MaxEmailLength = 50;
    public string Phone { get; }
    public string Email { get; }

    protected Contacts(string phone, string email = "")
    {
        Phone = phone;
        Email = email;
    }

    public static Result<Contacts> Create(string? phone, string? email)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return new Error(ContactsErrors.PhoneWasNull());
        if (!PhoneValidationHelper.IsPhoneValid(phone))
            return new Error(ContactsErrors.PhoneWasIncorrect());
        if (phone.Length > MaxPhoneLength)
            return new Error(ContactsErrors.PhoneExceedsMaxLength(MaxPhoneLength));
        if (!string.IsNullOrWhiteSpace(email) && !EmailValidationHelper.IsEmailValid(email))
            return new Error(ContactsErrors.EmailWasIncorrect());
        if (!string.IsNullOrWhiteSpace(email) && email.Length > MaxEmailLength)
            return new Error(ContactsErrors.EmailExceedsMaxLength(MaxEmailLength));
        return string.IsNullOrWhiteSpace(email)
            ? new PhoneOnlyContacts(phone)
            : new FullContacts(phone, email);
    }
}

public sealed record PhoneOnlyContacts : Contacts
{
    public PhoneOnlyContacts(string phone)
        : base(phone) { }
}

public sealed record FullContacts : Contacts
{
    public FullContacts(string phone, string email)
        : base(phone, email) { }
}

public static class ContactsErrors
{
    public static string PhoneWasNull() => "Phone was null";

    public static string PhoneWasIncorrect() => "Phone is incorrect";

    public static string EmailWasIncorrect() => "Email is incorrect";

    public static string PhoneExceedsMaxLength(int length) =>
        $"Phone is more than {length} characters";

    public static string EmailExceedsMaxLength(int length) =>
        $"Email is more than {length} characters";
}
