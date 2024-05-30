using M31.FluentApi.Generator.CodeGeneration;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

[Generator(LanguageNames.CSharp)]
internal class SourceGenerator : IIncrementalGenerator
{
    internal static readonly SourceGeneratorConfig GeneratorConfig = new SourceGeneratorConfig();

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var infos = context.SyntaxProvider
            .CreateSyntaxProvider(CanBeFluentApiClass, GetFluentApiClassInfo)
            .Where(info => info is not null)
            .Collect() // Handle partial classes, get an array of related info objects.
            .SelectMany((infos, _) =>
                infos.Distinct()); // Want to have every fluent API info object only once.

        context.RegisterSourceOutput(infos, SourceOutputAction!);
    }

    private void SourceOutputAction(SourceProductionContext ctx, FluentApiClassInfo classInfo)
    {
        string? generatedCode = GenerateCode(ctx, classInfo);

        if (generatedCode == null)
        {
            return;
        }

        string fileName = $"{classInfo.Namespace}.{classInfo.Name}.fluentapi.g.cs";
        ctx.AddSource(fileName, generatedCode);
    }

    private string? GenerateCode(SourceProductionContext ctx, FluentApiClassInfo classInfo)
    {
        try
        {
            CodeGeneratorResult codeGeneratorResult = CodeGenerator.GenerateCode(classInfo, ctx.CancellationToken);

            foreach (Diagnostic diagnostic in codeGeneratorResult.Diagnostics)
            {
                ctx.ReportDiagnostic(diagnostic);
            }

            return codeGeneratorResult.GenerationGotCancelled || codeGeneratorResult.HasErrors
                ? null
                : codeGeneratorResult.GeneratedCode;
        }
        catch (GenerationException generationException)
        {
            ctx.ReportDiagnostic(CodeGenerationException.CreateDiagnostic(generationException));
            return null;
        }
        catch (Exception exception)
        {
            ctx.ReportDiagnostic(GenericException.CreateDiagnostic(exception));
            return null;
        }
    }

    /// <summary>
    /// Transformation method. Creates the <see cref="FluentApiClassInfo"/> from the syntax context.
    /// </summary>
    /// <param name="ctx">The <see cref="GeneratorSyntaxContext"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="FluentApiClassInfo"/> or null in case of errors.</returns>
    private FluentApiClassInfo? GetFluentApiClassInfo(GeneratorSyntaxContext ctx, CancellationToken cancellationToken)
    {
        SyntaxNode? syntaxNode = ctx.Node.Parent?.Parent;

        if (!syntaxNode.IsTypeDeclarationOfInterest(out TypeDeclarationSyntax typeDeclaration))
        {
            return null;
        }

        ClassInfoResult result =
            ClassInfoFactory.CreateFluentApiClassInfo(
                ctx.SemanticModel, typeDeclaration, GeneratorConfig, cancellationToken);

        if (result.ClassInfoReport.HasErrors())
        {
            return null;
        }

        return result.ClassInfo;
    }

    /// <summary>
    /// Predicate / filter method. Must be fast.
    /// </summary>
    /// <param name="node">The <see cref="SyntaxNode"/> to analyze.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>True if the node potentially represents a fluent API class.</returns>
    private bool CanBeFluentApiClass(SyntaxNode node, CancellationToken cancellationToken)
    {
        if (node is not AttributeSyntax attributeSyntax)
        {
            return false;
        }

        // The parent of the attribute is a list of attributes, the parent of the parent is the class.
        SyntaxNode? syntaxNode = attributeSyntax.Parent?.Parent;

        if (!syntaxNode.IsTypeDeclarationOfInterest())
        {
            return false;
        }

        string? name = ExtractName(attributeSyntax.Name);

        // Note that we drop alias support for better performance.
        return name is "FluentApi" or "FluentApiAttribute";
    }

    private string? ExtractName(NameSyntax nameSyntax)
    {
        return nameSyntax switch
        {
            SimpleNameSyntax simpleNameSyntax => simpleNameSyntax.Identifier.Text, // without namespace
            QualifiedNameSyntax qualifiedNameSyntax => qualifiedNameSyntax.Right.Identifier.Text, // fully qualified
            _ => null
        };
    }
}