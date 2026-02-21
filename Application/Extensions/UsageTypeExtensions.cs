using GrmTask.Domain;

namespace GrmTask.Application.Extensions;

public static class UsageTypeExtensions
{
    public static string ToDisplayString(this UsageType usage) =>
        usage switch
        {
            UsageType.DigitalDownload => "digital download",
            UsageType.Streaming => "streaming",
            _ => usage.ToString()
        };
}