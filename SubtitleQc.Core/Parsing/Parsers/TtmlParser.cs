using System.Xml.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing.Parsers;

public sealed class TtmlParser : ISubtitleParser
{
    public SubtitleAsset Parse(string rawSubtitleContent, ShotChangeData? shotChanges = null)
    {
        XDocument doc = XDocument.Parse(rawSubtitleContent);
        List<Cue> cues = doc.Descendants().Where(e => e.Name.LocalName == "p").Select(ParseCue).ToList();
        return new SubtitleAsset(cues, shotChanges);
    }

    private static Cue ParseCue(XElement pElement)
    {
        TimeSpan start = TimecodeParser.Parse(GetAttribute(pElement, "begin"));
        TimeSpan end = TimecodeParser.Parse(GetAttribute(pElement, "end"));
        IReadOnlyList<string> lines = ExtractLines(pElement);
        return new Cue(Guid.NewGuid().ToString("N"), start, end, lines);
    }

    private static string GetAttribute(XElement element, string attributeName)
    {
        return element.Attributes().First(a => a.Name.LocalName == attributeName).Value;
    }

    private static IReadOnlyList<string> ExtractLines(XElement pElement)
    {
        string rawText = pElement.Value;
        return rawText.Split('\n', StringSplitOptions.None).Select(line => line.Trim()).ToList();
    }
}
