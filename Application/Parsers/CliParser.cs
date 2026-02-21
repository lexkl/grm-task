namespace GrmTask.Application.Parsers;

public sealed class CliParser
{
    public static (string Partner, DateTime EffectiveDate)? Parse(string[] args)
    {
        if (args == null || args.Length < 2)
            return null;

        string partner = args[0].Trim();
        if (string.IsNullOrEmpty(partner))
            return null;

        string dateStr = string.Join(" ", args.Skip(1)).Trim();
        if (string.IsNullOrEmpty(dateStr))
            return null;

        DateTime? effectiveDate = DateParser.Parse(dateStr);
        if (effectiveDate == null)
            return null;

        return (partner, effectiveDate.Value);
    }
}