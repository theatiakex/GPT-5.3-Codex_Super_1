using System.Collections.ObjectModel;

namespace SubtitleQc.Core.Qc;

public sealed class QcReport
{
    public QcReport(IReadOnlyList<QcResult> results)
    {
        Results = new ReadOnlyCollection<QcResult>(results.ToList());
        GeneratedAtUtc = DateTimeOffset.UtcNow;
    }

    public DateTimeOffset GeneratedAtUtc { get; }
    public IReadOnlyList<QcResult> Results { get; }
}
