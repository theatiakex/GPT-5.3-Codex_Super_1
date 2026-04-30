using System.Globalization;

namespace SubtitleQc.Core.Parsing;

internal static class TimecodeParser
{
    private static readonly string[] Formats =
    {
        @"hh\:mm\:ss\,fff",
        @"hh\:mm\:ss\.fff",
        @"mm\:ss\.fff",
        @"hh\:mm\:ss"
    };

    public static TimeSpan Parse(string value)
    {
        string normalized = value.Trim();
        return TimeSpan.ParseExact(normalized, Formats, CultureInfo.InvariantCulture);
    }
}
