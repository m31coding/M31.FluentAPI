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

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal class FluentApiAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(AllDescriptors);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
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

        ImmutableArray<SyntaxReference> syntaxReferences = symbol.DeclaringSyntaxReferences;

        if (syntaxReferences.Length == 0)
        {
            return;
        }

        SyntaxNode syntaxNode = syntaxReferences.First().GetSyntax();

        if (!syntaxNode.IsTypeDeclarationOfInterest(out TypeDeclarationSyntax typeDeclaration))
        {
            return;
        }

        if (ReportErrorDiagnosticForPartialKeyword(context, typeDeclaration, symbol))
        {
            return;
        }

        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (!symbol.InstanceConstructors.Any(m => m.Parameters.Length == 0))
        {
            context.ReportDiagnostic(MissingDefaultConstructor.CreateDiagnostic(symbol));
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

    private bool ReportErrorDiagnosticForPartialKeyword(
        SyntaxNodeAnalysisContext context,
        TypeDeclarationSyntax typeDeclaration,
        INamedTypeSymbol symbol)
    {
        SyntaxToken partialKeyword = typeDeclaration.Modifiers.FirstOrDefault(
            m => m.IsKind(SyntaxKind.PartialKeyword));

        if (partialKeyword != default)
        {
            context.ReportDiagnostic(UnsupportedPartialType.CreateDiagnostic(
                partialKeyword,
                symbol.Name));
            return true;
        }

        return false;
    }
}