using System.Text.RegularExpressions;

namespace PetFamily.Domain.Utils;

public static class PhoneValidationHelper
{
    private static readonly Regex PhoneValidationRegex = new Regex(
        @"^(\+?\d{1,3}[- ]?)?\d{10}$",
        RegexOptions.Compiled
    );

    public static bool IsPhoneValid(string phone) => PhoneValidationRegex.IsMatch(phone);
}
