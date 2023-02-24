using M31.FluentApi.Tests.CodeGeneration.Helpers;
using M31.FluentApi.Tests.Helpers;
using Xunit;

namespace M31.FluentApi.Tests.CodeGeneration;

public class CodeGenerationTests
{
    [Theory]
    [ClassData(typeof(TestDataProvider))]
    public void CanGenerateBuilderForAbstractTestClasses(params string[] testClassPathAndName)
    {
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        GeneratorOutput? generatorOutput = testClassCodeGenerator.RunGenerators();
        Assert.NotNull(generatorOutput);
        testClassCodeGenerator.WriteGeneratedCode(generatorOutput!);
        // testClassCodeGenerator.WriteGeneratedCodeAsExpectedCode(generatorOutput!);
        string expectedCode = testClassCodeGenerator.ReadExpectedCode(generatorOutput!.ClassName);
        Assert.Equal(expectedCode, generatorOutput.Code);
    }
}