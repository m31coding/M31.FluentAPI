using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration;

internal class CodeGeneratorResult
{
    private CodeGeneratorResult(
        string generatedCode,
        bool generationGotCancelled,
        IReadOnlyCollection<Diagnostic> diagnostics)
    {
        GeneratedCode = generatedCode;
        GenerationGotCancelled = generationGotCancelled;
        Diagnostics = diagnostics;
    }

    internal CodeGeneratorResult(string generatedCode, IReadOnlyCollection<Diagnostic> diagnostics)
        : this(generatedCode, false, diagnostics)
    {
        Diagnostics = diagnostics;
    }

    internal string GeneratedCode { get; }
    internal bool GenerationGotCancelled { get; }
    internal IReadOnlyCollection<Diagnostic> Diagnostics { get; }

    internal static CodeGeneratorResult Cancelled()
    {
        return new CodeGeneratorResult(null!, true, Array.Empty<Diagnostic>());
    }

    internal bool HasErrors => Diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error);
}