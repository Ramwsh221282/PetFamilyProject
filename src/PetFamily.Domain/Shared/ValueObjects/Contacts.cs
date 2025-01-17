using CSharpFunctionalExtensions;
using PetFamily.Domain.Utils;

namespace PetFamily.Domain.Shared.ValueObjects;

public abstract record Contacts
{
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
            return Result.Failure<Contacts>(PetOwnerContactsErrors.PhoneWasNull());
        if (!PhoneValidationHelper.IsPhoneValid(phone))
            return Result.Failure<Contacts>(PetOwnerContactsErrors.PhoneWasIncorrect());
        if (!string.IsNullOrWhiteSpace(email) && !EmailValidationHelper.IsEmailValid(email))
            return Result.Failure<Contacts>(PetOwnerContactsErrors.EmailWasIncorrect());
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

public static class PetOwnerContactsErrors
{
    public static string PhoneWasNull() => "Phone was null";

    public static string PhoneWasIncorrect() => "Phone is incorrect";

    public static string EmailWasIncorrect() => "Email is incorrect";
}
