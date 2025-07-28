namespace IdleWatch;

internal class Tools
{
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
}