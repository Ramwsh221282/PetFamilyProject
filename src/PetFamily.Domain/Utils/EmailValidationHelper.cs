using System.Text.RegularExpressions;

namespace PetFamily.Domain.Utils;

public static class EmailValidationHelper
{
    private static readonly Regex EmailValidationRegex = new Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled
    );

    public static bool IsEmailValid(string email) => EmailValidationRegex.IsMatch(email);
}
