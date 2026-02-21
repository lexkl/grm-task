using System.Globalization;
using System.Text.RegularExpressions;

namespace GrmTask.Application.Parsers;

public static partial class DateParser
{
    public static DateTime? Parse(string input)
    {
        var cleaned = MyRegex().Replace(input, "$1").Trim();

        if (DateTime.TryParse(cleaned, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            return result;
        }

        return null;
    }

    [GeneratedRegex(@"(\d+)(st|nd|rd|th)", RegexOptions.IgnoreCase)]
    private static partial Regex MyRegex();
}