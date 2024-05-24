using System;
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
        GeneratorOutputs generatorOutputs = testClassCodeGenerator.RunGenerators();
        Assert.NotNull(generatorOutputs.MainOutput);
        foreach (GeneratorOutput generatorOutput in generatorOutputs.Outputs)
        {
            testClassCodeGenerator.WriteGeneratedCodeIfChanged(generatorOutput);
        }
        // testClassCodeGenerator.WriteGeneratedCodeAsExpectedCode(generatorOutputs.MainOutput!);
        string expectedCode = testClassCodeGenerator.ReadExpectedCode(generatorOutputs.MainOutput!.ClassName);
        Assert.Equal(expectedCode, generatorOutputs.MainOutput.Code);
    }
}