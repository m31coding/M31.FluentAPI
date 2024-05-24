using System;
using System.IO;
using System.Linq;
using System.Threading;
using M31.FluentApi.Generator.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Tests.Helpers;

internal class TestClassCodeGenerator
{
    private TestClassCodeGenerator(string classPath, string className)
    {
        ClassPath = classPath;
        ClassName = className;
    }

    internal string ClassPath { get; }
    internal string ClassName { get; }

    internal static TestClassCodeGenerator Create(params string[] testClassPathAndName)
    {
        string classPath = Path.Join(testClassPathAndName[0..^1]);
        string className = testClassPathAndName[^1];
        return new TestClassCodeGenerator(classPath, className);
    }

    internal GeneratorOutputs RunGenerators()
    {
        string code = File.ReadAllText(PathToTestDataFile(ClassPath, $"{ClassName}.cs"));
        return ManualGenerator.RunGenerators(code);
    }

    internal (SemanticModel semanticModel, TypeDeclarationSyntax? typeDeclaration)
        GetSemanticModelAndTypeDeclaration()
    {
        string code = File.ReadAllText(PathToTestDataFile(ClassPath, $"{ClassName}.cs"));
        CSharpCompilation compilation = ManualGenerator.GetCompilation(code);
        SyntaxTree syntaxTree = compilation.SyntaxTrees.First();
        SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree);
        TypeDeclarationSyntax? typeDeclaration = syntaxTree.GetFluentApiTypeDeclaration();
        return (semanticModel, typeDeclaration);
    }

    internal ClassInfoResult CreateFluentApiClassInfoResult()
    {
        (SemanticModel semanticModel, TypeDeclarationSyntax? typeDeclaration) = GetSemanticModelAndTypeDeclaration();

        if (typeDeclaration == null)
        {
            throw new InvalidOperationException();
        }

        return ClassInfoFactory.CreateFluentApiClassInfo(
            semanticModel,
            typeDeclaration,
            SourceGenerator.GeneratorConfig,
            CancellationToken.None);
    }

    internal void WriteGeneratedCodeIfChanged(GeneratorOutput generatorOutput)
    {
        string file = PathToTestDataFile(ClassPath, $"{generatorOutput.ClassName}.g.cs");
        if (File.Exists(file))
        {
            string present = File.ReadAllText(file);
            if (present == generatorOutput.Code)
            {
                return;
            }
        }

        File.WriteAllText(file, generatorOutput.Code);
    }

    internal void WriteGeneratedCodeAsExpectedCode(GeneratorOutput generatorOutput)
    {
        File.WriteAllText(PathToTestDataFile(ClassPath, $"{generatorOutput.ClassName}.expected.txt"),
            generatorOutput.Code);
    }

    internal string ReadExpectedCode(string generatedClassName)
    {
        return File.ReadAllText(PathToTestDataFile(ClassPath, $"{generatedClassName}.expected.txt"));
    }

    private static string PathToTestDataFile(string classPath, string file)
    {
        return Path.Join(classPath, file);
    }
}