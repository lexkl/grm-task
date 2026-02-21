namespace GrmTask.Domain;

public sealed class MusicContract
{
    public string Artist { get; }
    public string Title { get; }
    public IReadOnlyCollection<UsageType> Usages { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }

    public MusicContract(
        string artist,
        string title,
        IReadOnlyCollection<UsageType> usages,
        DateTime startDate,
        DateTime? endDate)
    {
        Artist = artist;
        Title = title;
        Usages = usages;
        StartDate = startDate;
        EndDate = endDate;
    }

    public bool IsActiveOn(DateTime date) =>
        date >= StartDate &&
        (EndDate == null || date <= EndDate);
}