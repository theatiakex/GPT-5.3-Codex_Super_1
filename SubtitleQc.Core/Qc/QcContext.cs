using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class QcContext
{
    private readonly List<Cue> _cuesByStart;

    public QcContext(IReadOnlyList<Cue> cues, IShotChangeProvider? shotChangeProvider)
    {
        Cues = cues;
        _cuesByStart = cues.OrderBy(c => c.Start).ThenBy(c => c.End).ToList();
        ShotChangeProvider = shotChangeProvider;
    }

    public IReadOnlyList<Cue> Cues { get; }
    public IShotChangeProvider? ShotChangeProvider { get; }

    public Cue? GetPreviousCue(Cue cue)
    {
        int index = _cuesByStart.IndexOf(cue);
        return index <= 0 ? null : _cuesByStart[index - 1];
    }
}
