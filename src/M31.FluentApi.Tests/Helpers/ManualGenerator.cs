using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using M31.FluentApi.Generator.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace M31.FluentApi.Tests.Helpers;

internal static class ManualGenerator
{
    internal static CSharpCompilation GetCompilation(IReadOnlyCollection<string> sourceCode)
    {
        SyntaxTree[] inputSyntaxTrees = sourceCode.Select(c => CSharpSyntaxTree.ParseText(c)).ToArray();
        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic)
            .Select(assembly => MetadataReference
                .CreateFromFile(assembly.Location));

        CSharpCompilation compilation = CSharpCompilation.Create("SourceGeneratorTests",
            inputSyntaxTrees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return compilation;
    }

    internal static ImmutableArray<Diagnostic> RunGeneratorsAndGetDiagnostics(IReadOnlyCollection<string> sourceCode)
    {
        CSharpCompilation compilation = GetCompilation(sourceCode);
        SourceGenerator generator = new SourceGenerator();

        CSharpGeneratorDriver.Create(generator)
            .RunGeneratorsAndUpdateCompilation(compilation,
                out _,
                out var diagnostics);

        return diagnostics;
    }

    internal static GeneratorOutputs RunGenerators(IReadOnlyCollection<string> sourceCode)
    {
        CSharpCompilation compilation = GetCompilation(sourceCode);
        SourceGenerator generator = new SourceGenerator();
        SourceGenerator.GeneratorConfig.NewLineString = Environment.NewLine;

        CSharpGeneratorDriver.Create(generator)
            .RunGeneratorsAndUpdateCompilation(compilation,
                out var outputCompilation,
                out var diagnostics);

        Assert.Equal(0, diagnostics.Count(d => d.Severity == DiagnosticSeverity.Error));
        GeneratorOutput[] generatorOutputs =
            outputCompilation.SyntaxTrees
                .Skip(sourceCode.Count)
                .Select(GetGeneratorOutput)
                .OfType<GeneratorOutput>().ToArray();
        // First syntax trees are the input trees, the next syntax trees are the outputs of interest. For some
        // tests more than one generator output is created, e.g. for the FluentLambdaClass test.

        return new GeneratorOutputs(generatorOutputs);
    }

    private static GeneratorOutput? GetGeneratorOutput(SyntaxTree syntaxTree)
    {
        TypeDeclarationSyntax? typeDeclaration = syntaxTree.GetFluentApiTypeDeclaration();

        if (typeDeclaration == null)
        {
            return null;
        }

        return new GeneratorOutput(syntaxTree.ToString(), typeDeclaration.Identifier.Text);
    }
}