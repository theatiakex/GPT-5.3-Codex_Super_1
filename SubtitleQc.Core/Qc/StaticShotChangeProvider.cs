using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class StaticShotChangeProvider : IShotChangeProvider
{
    private readonly ShotChangeData _shotChangeData;

    public StaticShotChangeProvider(ShotChangeData shotChangeData)
    {
        _shotChangeData = shotChangeData;
    }

    public IReadOnlyList<TimeSpan> GetShotChangeTimestamps()
    {
        return _shotChangeData.CutTimestamps;
    }

    public IReadOnlyList<int> GetShotChangeFrames()
    {
        return _shotChangeData.CutFrames;
    }
}
