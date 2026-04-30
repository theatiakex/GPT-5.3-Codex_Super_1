using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IQcRule
{
    string RuleName { get; }
    RuleOutcome Evaluate(Cue cue, QcContext context);
}
