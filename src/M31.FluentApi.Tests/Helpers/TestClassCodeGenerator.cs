using System;
using System.Collections.Generic;
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
    private TestClassCodeGenerator(string classPath, IReadOnlyCollection<string> classNames)
    {
        ClassPath = classPath;
        ClassNames = classNames;
    }

    internal string ClassPath { get; }
    internal IReadOnlyCollection<string> ClassNames { get; }

    internal static TestClassCodeGenerator Create(params string[] testClassPathAndName)
    {
        string classPath = Path.Join(testClassPathAndName[0..^1]);
        string classes = testClassPathAndName[^1];
        string[] splitClasses = classes.Split('|');
        return new TestClassCodeGenerator(classPath, splitClasses);
    }

    internal GeneratorOutputs RunGenerators()
    {
        return ManualGenerator.RunGenerators(ReadSourceCode());
    }

    internal (SemanticModel semanticModel, TypeDeclarationSyntax? typeDeclaration)
        GetSemanticModelAndTypeDeclaration()
    {
        CSharpCompilation compilation = ManualGenerator.GetCompilation(ReadSourceCode());
        SyntaxTree syntaxTree = compilation.SyntaxTrees.First();
        SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree);
        TypeDeclarationSyntax? typeDeclaration = syntaxTree.GetFluentApiTypeDeclaration();
        return (semanticModel, typeDeclaration);
    }

    private string[] ReadSourceCode()
    {
        return ClassNames.Select(n => File.ReadAllText(PathToTestDataFile(ClassPath, $"{n}.cs"))).ToArray();
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