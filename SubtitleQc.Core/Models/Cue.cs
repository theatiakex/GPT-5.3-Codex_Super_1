using System.Collections.ObjectModel;

namespace SubtitleQc.Core.Models;

public sealed class Cue
{
    public Cue(
        string id,
        TimeSpan start,
        TimeSpan end,
        IReadOnlyList<string> lines,
        int? startFrame = null,
        int? endFrame = null,
        IReadOnlyDictionary<string, string>? attributes = null)
    {
        Id = id;
        Start = start;
        End = end;
        Lines = new ReadOnlyCollection<string>(lines.ToList());
        StartFrame = startFrame;
        EndFrame = endFrame;
        Attributes = attributes ?? new Dictionary<string, string>();
    }

    public string Id { get; }
    public TimeSpan Start { get; }
    public TimeSpan End { get; }
    public IReadOnlyList<string> Lines { get; }
    public int? StartFrame { get; }
    public int? EndFrame { get; }
    public IReadOnlyDictionary<string, string> Attributes { get; }
}
