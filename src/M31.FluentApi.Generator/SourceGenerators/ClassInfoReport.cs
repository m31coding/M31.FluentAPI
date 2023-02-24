using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class ClassInfoReport
{
    private readonly List<Diagnostic> diagnostics;
    internal IReadOnlyCollection<Diagnostic> Diagnostics => diagnostics;

    private readonly List<string> errors;
    internal IReadOnlyCollection<string> Errors => errors;

    internal ClassInfoReport()
    {
        diagnostics = new List<Diagnostic>();
        errors = new List<string>();
    }

    internal void ReportDiagnostic(Diagnostic diagnostic)
    {
        diagnostics.Add(diagnostic);
    }

    internal void ReportError(string error)
    {
        errors.Add(error);
    }

    internal bool HasErrors()
    {
        return errors.Count > 0 || diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error);
    }
}