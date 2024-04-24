// The symbol 'Environment' is banned for use by analyzers
#pragma warning disable RS1035

namespace M31.FluentApi.Generator.SourceGenerators;

internal class SourceGeneratorConfig
{
    internal string NewLineString { get; set; } = Environment.NewLine;
}