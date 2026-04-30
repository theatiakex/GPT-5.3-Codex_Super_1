using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinDurationRule : IQcRule
{
    private readonly TimeSpan _threshold;

    public MinDurationRule(TimeSpan threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MinDuration";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        bool isValid = (cue.End - cue.Start) >= _threshold;
        return isValid
            ? new RuleOutcome(RuleName, QcStatus.Passed, "Cue duration meets minimum threshold.")
            : new RuleOutcome(RuleName, QcStatus.Failed, "Cue duration is too short.");
    }
}
