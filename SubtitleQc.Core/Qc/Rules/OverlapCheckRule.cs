using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class OverlapCheckRule : IQcRule
{
    public string RuleName => "OverlapCheck";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        Cue? previousCue = context.GetPreviousCue(cue);
        bool overlapsPrevious = previousCue is not null && cue.Start < previousCue.End;
        return overlapsPrevious
            ? new RuleOutcome(RuleName, QcStatus.Failed, "Cue overlaps with previous cue.")
            : new RuleOutcome(RuleName, QcStatus.Passed, "No overlap detected.");
    }
}
