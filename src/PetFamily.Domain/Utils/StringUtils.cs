using System.Text;

namespace PetFamily.Domain.Utils;

public static class StringUtils
{
    public static string CapitalizeFirstLetter(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToUpper(value[0]));
        stringBuilder.Append(value.Substring(1));
        return stringBuilder.ToString();
    }
}
