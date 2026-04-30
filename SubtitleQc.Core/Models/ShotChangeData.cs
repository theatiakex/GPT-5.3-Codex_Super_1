using System.Collections.ObjectModel;

namespace SubtitleQc.Core.Models;

public sealed class ShotChangeData
{
    public ShotChangeData(IReadOnlyList<TimeSpan> cutTimestamps, IReadOnlyList<int> cutFrames)
    {
        CutTimestamps = new ReadOnlyCollection<TimeSpan>(cutTimestamps.ToList());
        CutFrames = new ReadOnlyCollection<int>(cutFrames.ToList());
    }

    public IReadOnlyList<TimeSpan> CutTimestamps { get; }
    public IReadOnlyList<int> CutFrames { get; }
}
