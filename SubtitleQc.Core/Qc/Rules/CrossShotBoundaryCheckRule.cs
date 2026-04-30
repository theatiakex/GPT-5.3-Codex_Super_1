using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class CrossShotBoundaryCheckRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;

    public CrossShotBoundaryCheckRule(IShotChangeProvider shotChangeProvider)
    {
        _shotChangeProvider = shotChangeProvider;
    }

    public string RuleName => "CrossShotBoundaryCheck";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        bool spansCut = _shotChangeProvider
            .GetShotChangeTimestamps()
            .Any(cut => cue.Start < cut && cut < cue.End);

        return spansCut
            ? new RuleOutcome(RuleName, QcStatus.Failed, "Cue spans across a shot cut.")
            : new RuleOutcome(RuleName, QcStatus.Passed, "Cue does not cross a shot cut.");
    }
}
