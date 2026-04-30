using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCplRule : IQcRule
{
    private readonly int _threshold;

    public MaxCplRule(int threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxCpl";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        bool isValid = cue.Lines.All(line => line.Length <= _threshold);
        return isValid
            ? new RuleOutcome(RuleName, QcStatus.Passed, "All lines are below CPL threshold.")
            : new RuleOutcome(RuleName, QcStatus.Failed, "At least one line exceeds CPL threshold.");
    }
}
