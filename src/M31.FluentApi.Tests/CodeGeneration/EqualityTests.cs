using System;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using M31.FluentApi.Tests.Helpers;
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
        Array.Reverse(testClassPathAndName);
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        ClassInfoResult result1 = testClassCodeGenerator.CreateFluentApiClassInfoResult();
        ClassInfoResult result2 = testClassCodeGenerator.CreateFluentApiClassInfoResult();
        Assert.NotNull(result1.ClassInfo);
        Assert.NotNull(result2.ClassInfo);
        Assert.Equal(result1.ClassInfo!.GetHashCode(), result2.ClassInfo!.GetHashCode());
    }

    [Theory]
    [ClassData(typeof(TestDataProvider))]
    public void TwoInstanceOfFluentApiClassInfoAreEqual(params string[] testClassPathAndName)
    {
        Array.Reverse(testClassPathAndName);
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        ClassInfoResult result1 = testClassCodeGenerator.CreateFluentApiClassInfoResult();
        ClassInfoResult result2 = testClassCodeGenerator.CreateFluentApiClassInfoResult();
        Assert.NotNull(result1.ClassInfo);
        Assert.NotNull(result2.ClassInfo);
        Assert.Equal(result1.ClassInfo, result2.ClassInfo);
    }
}