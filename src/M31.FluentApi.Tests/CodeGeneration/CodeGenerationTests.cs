using System;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using M31.FluentApi.Tests.Helpers;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public partial class CodeGenerationTests
{
    [Theory, Priority(0)]
    [ClassData(typeof(TestDataProvider))]
    public void CanGenerateBuilderForAbstractTestClasses(params string[] testClassPathAndName)
    {
        Array.Reverse(testClassPathAndName);
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        GeneratorOutput? generatorOutput = testClassCodeGenerator.RunGenerators();
        Assert.NotNull(generatorOutput);
        testClassCodeGenerator.WriteGeneratedCodeIfChanged(generatorOutput!);
        // testClassCodeGenerator.WriteGeneratedCodeAsExpectedCode(generatorOutput!);
        string expectedCode = testClassCodeGenerator.ReadExpectedCode(generatorOutput!.ClassName);
        Assert.Equal(expectedCode, generatorOutput.Code);
    }
}