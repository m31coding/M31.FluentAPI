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

    /// <summary>
    /// Report a diagnostic that will be shown to the user of the library.
    /// </summary>
    /// <param name="diagnostic">The diagnostic.</param>
    internal void ReportDiagnostic(Diagnostic diagnostic)
    {
        diagnostics.Add(diagnostic);
    }

    /// <summary>
    /// Report internal errors that don't make sense as a diagnostic.
    /// </summary>
    /// <param name="error">The error.</param>
    internal void ReportError(string error)
    {
        errors.Add(error);
    }

    /// <summary>
    /// Checks the report for errors and diagnostics with severity 'error'.
    /// </summary>
    /// <returns>True if there are errors or diagnostics with severity 'error'.</returns>
    internal bool HasErrors()
    {
        return errors.Count > 0 || diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error);
    }
}