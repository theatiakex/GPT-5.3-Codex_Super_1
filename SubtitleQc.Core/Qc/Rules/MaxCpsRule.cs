using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCpsRule : IQcRule
{
    private readonly double _threshold;

    public MaxCpsRule(double threshold)
    {
        _threshold = threshold;
    }

    public string RuleName => "MaxCps";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        double cps = CalculateCharactersPerSecond(cue);
        bool isValid = cps <= _threshold;
        return isValid
            ? new RuleOutcome(RuleName, QcStatus.Passed, "CPS is within threshold.")
            : new RuleOutcome(RuleName, QcStatus.Failed, "CPS exceeds allowed threshold.");
    }

    private static double CalculateCharactersPerSecond(Cue cue)
    {
        int totalChars = cue.Lines.Sum(line => line.Length);
        double seconds = Math.Max((cue.End - cue.Start).TotalSeconds, 0.001);
        return totalChars / seconds;
    }
}
