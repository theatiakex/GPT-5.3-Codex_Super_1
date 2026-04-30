using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Parsing.Abstractions;

public interface ISubtitleParser
{
    SubtitleAsset Parse(string rawSubtitleContent, ShotChangeData? shotChanges = null);
}
