using M31.FluentApi.Generator.CodeGeneration;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

[Generator(LanguageNames.CSharp)]
internal class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var infos = context.SyntaxProvider
            .CreateSyntaxProvider(CanBeFluentApiClass, GetFluentApiClassInfo)
            .Where(info => info is not null)
            .Collect() // handle partial classes, get an array of related info objects into GenerateCode
            .SelectMany((infos, _) =>
                infos.Distinct()); // want to have every fluent API info object only once.

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

    private FluentApiClassInfo? GetFluentApiClassInfo(GeneratorSyntaxContext ctx, CancellationToken cancellationToken)
    {
        SyntaxNode? syntaxNode = ctx.Node.Parent?.Parent;

        if (!syntaxNode.IsTypeDeclarationOfInterest(out TypeDeclarationSyntax typeDeclaration))
        {
            return null;
        }

        ClassInfoResult result =
            ClassInfoFactory.CreateFluentApiClassInfo(ctx.SemanticModel, typeDeclaration, cancellationToken);

        if (result.ClassInfoReport.HasErrors())
        {
            return null;
        }

        return result.ClassInfo;
    }

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