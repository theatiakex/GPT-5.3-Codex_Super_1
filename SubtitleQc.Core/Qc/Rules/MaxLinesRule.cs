using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxLinesRule : IQcRule
{
    private readonly int _threshold;

    public MaxLinesRule(int threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxLines";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        bool isValid = cue.Lines.Count <= _threshold;
        return isValid
            ? new RuleOutcome(RuleName, QcStatus.Passed, "Line count is within limit.")
            : new RuleOutcome(RuleName, QcStatus.Failed, "Cue exceeds max lines threshold.");
    }
}
