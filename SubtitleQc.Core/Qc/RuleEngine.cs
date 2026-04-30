using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class RuleEngine
{
    private readonly IReadOnlyList<IQcRule> _rules;
    private readonly IShotChangeProvider? _shotChangeProvider;

    public RuleEngine(IEnumerable<IQcRule> rules, IShotChangeProvider? shotChangeProvider = null)
    {
        _rules = rules.ToList();
        _shotChangeProvider = shotChangeProvider;
    }

    public QcReport Evaluate(IEnumerable<Cue> cues)
    {
        var cueList = cues.ToList();
        var context = new QcContext(cueList, _shotChangeProvider);
        var results = cueList.Select(cue => EvaluateCue(cue, context)).ToList();
        return new QcReport(results);
    }

    private QcResult EvaluateCue(Cue cue, QcContext context)
    {
        List<RuleOutcome> outcomes = _rules.Select(rule => rule.Evaluate(cue, context)).ToList();
        return new QcResult(cue.Id, outcomes);
    }
}
