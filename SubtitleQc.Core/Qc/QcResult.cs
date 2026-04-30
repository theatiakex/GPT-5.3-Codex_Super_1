using System.Collections.ObjectModel;

namespace SubtitleQc.Core.Qc;

public sealed class QcResult
{
    public QcResult(string cueId, IReadOnlyList<RuleOutcome> ruleOutcomes)
    {
        CueId = cueId;
        RuleOutcomes = new ReadOnlyCollection<RuleOutcome>(ruleOutcomes.ToList());
        Status = RuleOutcomes.Any(r => r.Status == QcStatus.Failed) ? QcStatus.Failed : QcStatus.Passed;
    }

    public string CueId { get; }
    public QcStatus Status { get; }
    public IReadOnlyList<RuleOutcome> RuleOutcomes { get; }
}
