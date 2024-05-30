using System.Collections.Generic;
using System.Linq;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Tests.Helpers;
using Xunit;

namespace M31.FluentApi.Tests.CodeGeneration;

/// <summary>
/// These tests ensure that the caching of the incremental source generator works.
/// </summary>
public class MethodIdentityTests
{
    [Fact]
    public void CanCreateMethodIdentities()
    {
        string[] testClassPathAndName = new string[]
        {
            "..", "..", "..", "CodeGeneration", "TestClasses", "Abstract", "GenericOverloadedMethodClass",
            "Student"
        };
        TestClassCodeGenerator testClassCodeGenerator = TestClassCodeGenerator.Create(testClassPathAndName);
        ClassInfoResult classInfoResult = testClassCodeGenerator.CreateFluentApiClassInfoResult();
        Assert.NotNull(classInfoResult.ClassInfo);
        Assert.False(classInfoResult.ClassInfoReport.HasErrors());

        MethodSymbolInfo[] methodSymbolInfos = classInfoResult.ClassInfo!.FluentApiInfos.Select(i => i.SymbolInfo)
            .OfType<MethodSymbolInfo>().ToArray();
        MethodIdentity[] methodIdentities = methodSymbolInfos.Select(MethodIdentity.Create).ToArray();
        string[] expected = new string[]
        {
            "Method1<0>(int, string)",
            "Method1<1>(int, string)",
            "Method1<1>(g0, string)",
            "Method1<2>(g1, string)",
            "Method1<2>(g1, mod-string)",
            "Method1<2>(mod-g1, string)",
            "Method1<2>(mod-g1, mod-string)"
        };
        Assert.Equal(expected, methodIdentities.Select(i => i.ToString()));

        HashSet<MethodIdentity> set = new HashSet<MethodIdentity>(methodIdentities);
        Assert.Equal(methodIdentities.Length, set.Count);
    }
}