using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class EmptyCueCheckRule : IQcRule
{
    public string RuleName => "EmptyCueCheck";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        bool hasText = cue.Lines.Any(line => !string.IsNullOrWhiteSpace(line));
        return hasText
            ? new RuleOutcome(RuleName, QcStatus.Passed, "Cue contains visible text.")
            : new RuleOutcome(RuleName, QcStatus.Failed, "Cue content is empty.");
    }
}
