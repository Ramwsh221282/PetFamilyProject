using PetFamily.Domain.Utils;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Shared.ValueObjects;

public abstract record Contacts
{
    public const int MaxPhoneLength = 50;
    public const int MaxEmailLength = 50;
    public string Phone { get; }
    public string Email { get; }

    protected Contacts(string phone, string? email = null)
    {
        Phone = phone;
        Email = email ?? "";
    }

    public static Result<Contacts> Create(string? phone, string? email) => new ResultPipe()
        .Check(string.IsNullOrWhiteSpace(phone), ContactsErrors.PhoneWasNull)
        .Check(!string.IsNullOrWhiteSpace(phone) && phone.Length > MaxPhoneLength, ContactsErrors.PhoneExceedsMaxLength)
        .Check(!string.IsNullOrWhiteSpace(phone) && !PhoneValidationHelper.IsPhoneValid(phone), ContactsErrors.PhoneWasIncorrect)
        .Check(!string.IsNullOrWhiteSpace(email) && !EmailValidationHelper.IsEmailValid(email),
            ContactsErrors.EmailWasIncorrect)
        .Check(!string.IsNullOrWhiteSpace(email) && email.Length > MaxEmailLength, ContactsErrors.EmailExceedsMaxLength)
        .FromPipe(() => 
        {
            Contacts contacts = string.IsNullOrWhiteSpace(email)
                ? new PhoneOnlyContacts(phone!)
                : new FullContacts(phone!, email);
            return contacts;
        });
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
    public static Error PhoneWasNull => 
        new("Phone was null", ErrorStatusCode.BadRequest);
    public static Error PhoneWasIncorrect => 
        new("Phone is incorrect", ErrorStatusCode.BadRequest);
    public static Error EmailWasIncorrect => 
        new("Email is incorrect", ErrorStatusCode.BadRequest);
    public static Error PhoneExceedsMaxLength =>
        new($"Phone is more than {Contacts.MaxPhoneLength} characters", ErrorStatusCode.BadRequest);
    public static Error EmailExceedsMaxLength =>
        new($"Email is more than {Contacts.MaxEmailLength} characters", ErrorStatusCode.BadRequest);
}
