using SubtitleQc.Core.Parsing.Abstractions;
using SubtitleQc.Core.Parsing.Parsers;

namespace SubtitleQc.Core.Parsing;

public static class SubtitleParserFactory
{
    public static ISubtitleParser CreateForExtension(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".srt" => new SrtParser(),
            ".vtt" => new WebVttParser(),
            ".ttml" or ".xml" => new TtmlParser(),
            _ => throw new NotSupportedException($"Unsupported subtitle extension: {extension}")
        };
    }
}
