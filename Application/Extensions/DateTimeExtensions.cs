using System.Globalization;

namespace GrmTask.Application.Extensions;

public static class DateTimeExtensions
{
    private static string OrdinalSuffix(int day) => day switch
    {
        1 or 21 or 31 => "st",
        2 or 22 => "nd",
        3 or 23 => "rd",
        _ => "th"
    };

    public static string ToOrdinalDateString(this DateTime date)
    {
        var day = date.Day;
        var suffix = OrdinalSuffix(day);
        var monthYear = date.ToString("MMM yyyy", CultureInfo.InvariantCulture);
        return $"{day}{suffix} {monthYear}";
    }
}
