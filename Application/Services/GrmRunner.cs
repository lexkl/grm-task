using GrmTask.Domain;
using GrmTask.Application.Interfaces;

namespace GrmTask.Application.Services;

public sealed class GrmRunner
{
    private readonly IMusicContractLoader _musicLoader;
    private readonly IPartnerContractLoader _partnerLoader;
    private readonly ContractFilterService _filterService;

    public GrmRunner(
        IMusicContractLoader musicLoader,
        IPartnerContractLoader partnerLoader,
        ContractFilterService filterService)
    {
        _musicLoader = musicLoader;
        _partnerLoader = partnerLoader;
        _filterService = filterService;
    }

    public IEnumerable<(MusicContract Contract, UsageType Usage)> Run(
        string partnerName, DateTime effectiveDate)
    {
        var musicContracts = _musicLoader.Load("musicContracts.txt");
        var partnerContracts = _partnerLoader.Load("partnerContracts.txt");

        var partnerContract = partnerContracts
            .FirstOrDefault(p =>
                p.Partner.Equals(partnerName, StringComparison.OrdinalIgnoreCase));

        if (partnerContract == null)
            throw new InvalidOperationException(
                $"Partner '{partnerName}' not found.");

        var partnerUsage = partnerContract.Usage;

        return _filterService.GetActiveContracts(
            musicContracts,
            partnerUsage,
            effectiveDate);
    }
}