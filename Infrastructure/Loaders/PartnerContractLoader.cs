using GrmTask.Application.Interfaces;
using GrmTask.Domain;

namespace GrmTask.Infrastructure.Loaders;

public sealed class PartnerContractLoader : IPartnerContractLoader
{
    public IEnumerable<PartnerContract> Load(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException(
                $"Partner contracts file not found: {path}");

        foreach (var line in File.ReadLines(path).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');

            if (parts.Length < 2)
                continue;

            var partner = parts[0].Trim();
            if (string.IsNullOrEmpty(partner))
                continue;

            var usageText = parts[1].Trim();

            if (!TryParseUsage(usageText, out var usage))
                continue;

            yield return new PartnerContract(partner, usage);
        }
    }

    private static bool TryParseUsage(
        string input,
        out UsageType usage)
    {
        switch (input.Trim().ToLowerInvariant())
        {
            case "digital download":
                usage = UsageType.DigitalDownload;
                return true;

            case "streaming":
                usage = UsageType.Streaming;
                return true;

            default:
                usage = default;
                return false;
        }
    }
}