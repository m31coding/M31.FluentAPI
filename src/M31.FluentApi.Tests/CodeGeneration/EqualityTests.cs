using System.Threading;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using M31.FluentApi.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace M31.FluentApi.Tests.CodeGeneration;

/// <summary>
/// These tests ensure that the caching of the incremental source generator works.
/// </summary>
public class EqualityTests
{
    [Theory]
    [ClassData(typeof(TestDataProvider))]
    public void TwoInstanceOfFluentApiClassInfoHaveTheSameHashCode(params string[] testClassPathAndName)
    {
        ClassInfoResult result1 = CreateFluentApiClassInfoResult(testClassPathAndName);
        ClassInfoResult result2 = CreateFluentApiClassInfoResult(testClassPathAndName);
        Assert.NotNull(result1.ClassInfo);
        Assert.NotNull(result2.ClassInfo);
        Assert.Equal(result1.ClassInfo!.GetHashCode(), result2.ClassInfo!.GetHashCode());
    }

    [Theory]
    [ClassData(typeof(TestDataProvider))]
    public void TwoInstanceOfFluentApiClassInfoAreEqual(params string[] testClassPathAndName)
    {
        ClassInfoResult result1 = CreateFluentApiClassInfoResult(testClassPathAndName);
        ClassInfoResult result2 = CreateFluentApiClassInfoResult(testClassPathAndName);
        Assert.NotNull(result1.ClassInfo);
        Assert.NotNull(result2.ClassInfo);
        Assert.Equal(result1.ClassInfo, result2.ClassInfo);
    }

    private ClassInfoResult CreateFluentApiClassInfoResult(string[] testClassPathAndName)
    {
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        (SemanticModel semanticModel, TypeDeclarationSyntax? typeDeclaration) =
            testClassCodeGenerator.GetSemanticModelAndTypeDeclaration();
        Assert.NotNull(typeDeclaration);
        return ClassInfoFactory.CreateFluentApiClassInfo(semanticModel, typeDeclaration!, CancellationToken.None);
    }
}