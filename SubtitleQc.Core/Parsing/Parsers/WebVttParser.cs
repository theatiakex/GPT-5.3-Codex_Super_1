using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing.Parsers;

public sealed class WebVttParser : ISubtitleParser
{
    public SubtitleAsset Parse(string rawSubtitleContent, ShotChangeData? shotChanges = null)
    {
        List<Cue> cues = ParseCueBlocks(rawSubtitleContent).Select(ParseCue).ToList();
        return new SubtitleAsset(cues, shotChanges);
    }

    private static IEnumerable<string[]> ParseCueBlocks(string rawSubtitleContent)
    {
        string normalized = rawSubtitleContent.Replace("\r", string.Empty).Trim();
        string withoutHeader = normalized.StartsWith("WEBVTT", StringComparison.OrdinalIgnoreCase)
            ? normalized[(normalized.IndexOf('\n') + 1)..]
            : normalized;
        return withoutHeader.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Select(b => b.Split('\n'));
    }

    private static Cue ParseCue(string[] lines)
    {
        int timingLineIndex = lines[0].Contains("-->", StringComparison.Ordinal) ? 0 : 1;
        (TimeSpan start, TimeSpan end) = ParseTiming(lines[timingLineIndex]);
        var textLines = lines.Skip(timingLineIndex + 1).ToList();
        return new Cue(Guid.NewGuid().ToString("N"), start, end, textLines);
    }

    private static (TimeSpan start, TimeSpan end) ParseTiming(string timingLine)
    {
        string[] parts = timingLine.Split("-->", StringSplitOptions.TrimEntries);
        return (TimecodeParser.Parse(parts[0]), TimecodeParser.Parse(parts[1]));
    }
}
