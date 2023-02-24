namespace M31.FluentApi.Generator.Commons;

internal static class StringExtensions
{
    internal static string FirstCharToLower(this string str)
    {
        if (str == string.Empty) return str;
        char[] chars = str.ToCharArray();
        chars[0] = char.ToLower(chars[0]);
        return new string(chars);
    }

    internal static string FirstCharToUpper(this string str)
    {
        if (str == string.Empty) return str;
        char[] chars = str.ToCharArray();
        chars[0] = char.ToUpper(chars[0]);
        return new string(chars);
    }
}