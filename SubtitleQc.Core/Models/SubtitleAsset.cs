using System.Collections.ObjectModel;

namespace SubtitleQc.Core.Models;

public sealed class SubtitleAsset
{
    public SubtitleAsset(IReadOnlyList<Cue> cues, ShotChangeData? shotChanges = null)
    {
        Cues = new ReadOnlyCollection<Cue>(cues.ToList());
        ShotChanges = shotChanges ?? new ShotChangeData(Array.Empty<TimeSpan>(), Array.Empty<int>());
    }

    public IReadOnlyList<Cue> Cues { get; }
    public ShotChangeData ShotChanges { get; }
}
