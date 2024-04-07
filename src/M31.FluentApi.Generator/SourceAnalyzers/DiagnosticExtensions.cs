using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceAnalyzers;

internal static class DiagnosticExtensions
{
    internal static bool HaveErrors(this IEnumerable<Diagnostic> diagnostics)
    {
        return diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error);
    }
}