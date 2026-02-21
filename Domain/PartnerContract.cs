namespace GrmTask.Domain;

public sealed class PartnerContract
{
    public string Partner { get; }
    public UsageType Usage { get; }

    public PartnerContract(string partner, UsageType usage)
    {
        Partner = partner;
        Usage = usage;
    }
}