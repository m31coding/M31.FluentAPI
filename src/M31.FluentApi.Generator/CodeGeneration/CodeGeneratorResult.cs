using M31.FluentApi.Generator.SourceAnalyzers;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration;

internal class CodeGeneratorResult
{
    private readonly string? generatedCode;

    private CodeGeneratorResult(
        string? generatedCode,
        bool generationGotCancelled,
        IReadOnlyCollection<Diagnostic> diagnostics)
    {
        this.generatedCode = generatedCode;
        GenerationGotCancelled = generationGotCancelled;
        Diagnostics = diagnostics;
    }

    internal CodeGeneratorResult(string generatedCode, IReadOnlyCollection<Diagnostic> diagnostics)
        : this(generatedCode, false, diagnostics)
    {
        if (diagnostics.HaveErrors())
        {
            throw new ArgumentException("Expected diagnostics without errors.");
        }
    }

    internal string GeneratedCode => GenerationGotCancelled || HasErrors
        ? throw new InvalidOperationException("Generation got cancelled or has errors.")
        : generatedCode!;

    internal bool GenerationGotCancelled { get; }
    internal IReadOnlyCollection<Diagnostic> Diagnostics { get; }

    internal static CodeGeneratorResult Cancelled()
    {
        return new CodeGeneratorResult(null, true, Array.Empty<Diagnostic>());
    }

    internal static CodeGeneratorResult WithErrors(IReadOnlyCollection<Diagnostic> diagnostics)
    {
        if (!diagnostics.HaveErrors())
        {
            throw new ArgumentException("Expected diagnostics with errors.");
        }

        return new CodeGeneratorResult(null, false, diagnostics);
    }

    internal bool HasErrors => Diagnostics.HaveErrors();
}