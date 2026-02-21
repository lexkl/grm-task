using GrmTask.Application.Interfaces;
using GrmTask.Application.Parsers;
using GrmTask.Domain;

namespace GrmTask.Infrastructure.Loaders;

public sealed class MusicContractLoader : IMusicContractLoader
{
    public IEnumerable<MusicContract> Load(string path)
    {
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length < 5) continue;

            var artist = parts[0].Trim();
            if (string.IsNullOrEmpty(artist)) continue;

            var title = parts[1].Trim();
            if (string.IsNullOrEmpty(title)) continue;

            var usages = parts[2].Trim();
            if (string.IsNullOrEmpty(usages)) continue;

            List<UsageType> usagesList = usages!
                .Split(',')
                .Select(ParseUsage)
                .Where(u => u.HasValue)
                .Select(u => u!.Value)
                .ToList();

            var startDate = DateParser.Parse(parts[3]);
            if (!startDate.HasValue) continue;

            DateTime? endDate =
                string.IsNullOrWhiteSpace(parts[4])
                    ? null
                    : DateParser.Parse(parts[4]);

            yield return new MusicContract(
                artist,
                title,
                usagesList,
                startDate.Value,
                endDate);
        }
    }

    private static UsageType? ParseUsage(string s) =>
        s.Trim().ToLower() switch
        {
            "digital download" => UsageType.DigitalDownload,
            "streaming" => UsageType.Streaming,
            _ => null
        };
}