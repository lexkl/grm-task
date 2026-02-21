using GrmTask.Application.Interfaces;
using GrmTask.Application.Parsers;
using GrmTask.Application.Services;
using GrmTask.Infrastructure.Loaders;
using GrmTask.Presentation;

namespace GrmTask;

internal static class Program
{
    public static int Main(string[] args)
    {
        var parsed = CliParser.Parse(args);

        if (parsed == null)
        {
            Console.Error.WriteLine("Usage: GrmTask <PartnerName> <EffectiveDate>");
            return 1;
        }

        var (partner, effectiveDate) = parsed.Value;

        try
        {
            IMusicContractLoader musicLoader = new MusicContractLoader();
            IPartnerContractLoader partnerLoader = new PartnerContractLoader();
            var filterService = new ContractFilterService();

            var runner = new GrmRunner(musicLoader, partnerLoader, filterService);
            var results = runner.Run(partner, effectiveDate);

            ConsoleRenderer.Render(results);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }

        return 0;
    }
}
