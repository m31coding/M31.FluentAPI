using System.Collections.Immutable;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;
using static M31.FluentApi.Generator.SourceGenerators.AttributeElements.Attributes.FullNames;

namespace M31.FluentApi.Generator.SourceAnalyzers;

// todo: https://github.com/dotnet/roslyn-analyzers/issues/7438
[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal class FluentApiAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(AllDescriptors);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(
            AnalyzeNode,
            SyntaxKind.ClassDeclaration,
            SyntaxKind.StructDeclaration,
            SyntaxKind.RecordDeclaration,
            SyntaxKind.RecordStructDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        try
        {
            AnalyzeNodeInternal(context);
        }
        catch (GenerationException generationException)
        {
            context.ReportDiagnostic(CodeGenerationException.CreateDiagnostic(generationException));
        }
        catch (Exception exception)
        {
            context.ReportDiagnostic(GenericException.CreateDiagnostic(exception));
        }
    }

    private void AnalyzeNodeInternal(SyntaxNodeAnalysisContext context)
    {
        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (context.ContainingSymbol is not INamedTypeSymbol symbol || !symbol.HasAttribute(FluentApiAttribute))
        {
            return;
        }

        if (context.Node is not TypeDeclarationSyntax typeDeclaration)
        {
            return;
        }

        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        ClassInfoResult classInfoResult =
            ClassInfoFactory.CreateFluentApiClassInfo(
                context.SemanticModel,
                typeDeclaration,
                SourceGenerator.GeneratorConfig,
                context.CancellationToken);

        foreach (Diagnostic diagnostic in classInfoResult.ClassInfoReport.Diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }
    }
}