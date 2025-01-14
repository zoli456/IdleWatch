using System.Text.RegularExpressions;

namespace IdleWatch;

internal class Tools
{
    internal static bool Is_between_one_and_hundred(string input)
    {
        input.Trim();
        if (string.IsNullOrWhiteSpace(input)) return false;
        return Regex.IsMatch(input, "^([1-9][0-9]?|100)$");
    }

    internal static bool Is_higher_than_one(string input)
    {
        input.Trim();
        int temp;
        if (string.IsNullOrWhiteSpace(input)) return false;
        try
        {
            temp = int.Parse(input);
        }
        catch (FormatException)
        {
            return false;
        }

        return temp > 0;
    }

    internal static bool Is_it_bool(string input)
    {
        input.Trim();
        bool parsedValue = bool.TryParse(input, out parsedValue) && parsedValue;
        return parsedValue;
    }
}