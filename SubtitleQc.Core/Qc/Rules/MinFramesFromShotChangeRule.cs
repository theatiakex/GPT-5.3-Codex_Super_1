using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinFramesFromShotChangeRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    private readonly int _thresholdFrames;

    public MinFramesFromShotChangeRule(IShotChangeProvider shotChangeProvider, int thresholdFrames)
    {
        _shotChangeProvider = shotChangeProvider;
        _thresholdFrames = thresholdFrames;
    }

    public string RuleName => "MinFramesFromShotChange";

    public RuleOutcome Evaluate(Cue cue, QcContext context)
    {
        if (!cue.StartFrame.HasValue)
        {
            return new RuleOutcome(RuleName, QcStatus.Passed, "Start frame is unavailable.");
        }

        bool tooClose = _shotChangeProvider.GetShotChangeFrames().Any(cut => IsTooClose(cue.StartFrame.Value, cut));
        return tooClose
            ? new RuleOutcome(RuleName, QcStatus.Failed, "Cue starts too close to a shot cut.")
            : new RuleOutcome(RuleName, QcStatus.Passed, "Cue starts far enough from shot cuts.");
    }

    private bool IsTooClose(int cueStartFrame, int cutFrame)
    {
        int distance = cueStartFrame - cutFrame;
        return distance >= 0 && distance < _thresholdFrames;
    }
}
