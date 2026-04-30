## Super

Implemented a working prototype from agents_super.md + superspec.md with a clean split between unified models, format parsers, and QC rules.

What I built
Added new core library at SubtitleQc.Core/SubtitleQc.Core.csproj targeting net10.0.
Implemented unified internal model in SubtitleQc.Core/Models/:
Cue
SubtitleAsset
ShotChangeData
Implemented QC engine + abstractions in SubtitleQc.Core/Qc/:
RuleEngine, QcContext, QcReport, QcResult, RuleOutcome, QcStatus
IQcRule, IShotChangeProvider, and StaticShotChangeProvider
Implemented all required QC rules in SubtitleQc.Core/Qc/Rules/:
MaxLinesRule
MaxCplRule
MaxCpsRule
MinDurationRule
OverlapCheckRule
EmptyCueCheckRule
CrossShotBoundaryCheckRule
MinFramesFromShotChangeRule
Added parser layer (decoupled from rule engine) in SubtitleQc.Core/Parsing/:
ISubtitleParser
SrtParser
WebVttParser
TtmlParser
SubtitleParserFactory
TimecodeParser
Project wiring
Updated SubtitleQc.Tests/SubtitleQc.Tests.csproj to:
reference SubtitleQc.Core
add concrete test package versions (restore was failing due missing versions)
Verification
Ran dotnet test SubtitleQc.Tests/SubtitleQc.Tests.csproj
Result: 19 passed, 0 failed
Telemetry files required by your agent spec
Created/updated test_metrics.jsonl with three attempts (including failed restore/build attempts and final pass).
Generated test_performance_report.md with a Mermaid pass-rate chart when pass rate reached 100%.
If you want, I can now add a tiny CLI entry point that accepts .srt/.vtt/.ttml + optional shot-change JSON and emits a JSON QC report using this prototype.