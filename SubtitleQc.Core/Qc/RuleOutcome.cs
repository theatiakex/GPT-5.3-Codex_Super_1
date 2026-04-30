namespace SubtitleQc.Core.Qc;

public sealed class RuleOutcome
{
    public RuleOutcome(string ruleName, QcStatus status, string message)
    {
        RuleName = ruleName;
        Status = status;
        Message = message;
    }

    public string RuleName { get; }
    public QcStatus Status { get; }
    public string Message { get; }
}
