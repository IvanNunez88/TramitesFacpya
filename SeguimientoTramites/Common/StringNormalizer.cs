using System.Text.RegularExpressions;

namespace SeguimientoTramites.Common;

public static class StringNormalizer
{
    private static readonly Regex MultipleSpaces = new(@"\s+", RegexOptions.Compiled);

    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return MultipleSpaces.Replace(value.Trim(), " ");
    }
}
