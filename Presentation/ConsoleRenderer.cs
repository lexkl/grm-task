using GrmTask.Domain;
using GrmTask.Application.Extensions;

namespace GrmTask.Presentation;

public sealed class ConsoleRenderer
{
    public static void Render(
        IEnumerable<(MusicContract Contract, UsageType Usage)> results)
    {
        Console.WriteLine("Artist|Title|Usage|StartDate|EndDate");

        foreach (var (contract, usage) in results)
        {
            var start = contract.StartDate.ToOrdinalDateString();
            var end = contract.EndDate.HasValue
                ? contract.EndDate.Value.ToOrdinalDateString()
                : "";

            Console.WriteLine($"{contract.Artist}|{contract.Title}|{usage.ToDisplayString()}|{start}|{end}");
        }
    }
}