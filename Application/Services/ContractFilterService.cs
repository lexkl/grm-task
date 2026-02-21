using GrmTask.Domain;

namespace GrmTask.Application.Services;

public sealed class ContractFilterService
{
    public IEnumerable<(MusicContract Contract, UsageType Usage)>
        GetActiveContracts(
            IEnumerable<MusicContract> contracts,
            UsageType partnerUsage,
            DateTime date)
    {
        return contracts
            .Where(c => c.IsActiveOn(date))
            .Where(c => c.Usages.Contains(partnerUsage))
            .Select(c => (Contract: c, Usage: partnerUsage))
            .OrderBy(x => x.Contract.Artist)
            .ThenBy(x => x.Contract.Title);
    }
}