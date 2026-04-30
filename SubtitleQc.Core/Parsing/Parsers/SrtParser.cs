using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing.Parsers;

public sealed class SrtParser : ISubtitleParser
{
    public SubtitleAsset Parse(string rawSubtitleContent, ShotChangeData? shotChanges = null)
    {
        List<Cue> cues = SplitBlocks(rawSubtitleContent).Select(ParseBlock).Where(c => c is not null).Cast<Cue>().ToList();
        return new SubtitleAsset(cues, shotChanges);
    }

    private static IEnumerable<string[]> SplitBlocks(string rawSubtitleContent)
    {
        string normalized = rawSubtitleContent.Replace("\r", string.Empty);
        return normalized.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Select(block => block.Split('\n'));
    }

    private static Cue? ParseBlock(string[] lines)
    {
        if (lines.Length < 3)
        {
            return null;
        }

        string timingLine = lines[1];
        (TimeSpan start, TimeSpan end) = ParseTiming(timingLine);
        var textLines = lines.Skip(2).ToList();
        return new Cue(Guid.NewGuid().ToString("N"), start, end, textLines);
    }

    private static (TimeSpan start, TimeSpan end) ParseTiming(string timingLine)
    {
        string[] parts = timingLine.Split("-->", StringSplitOptions.TrimEntries);
        return (TimecodeParser.Parse(parts[0]), TimecodeParser.Parse(parts[1]));
    }
}
