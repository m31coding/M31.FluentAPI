// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodClass4;

[FluentApi]
public class Student
{
    [FluentMethod(0, "Method1")]
    private void Method2(int p1, ref string p2)
    {
    }

    [FluentMethod(0, "Method1")]
    private void Method3(int p1, in string p2)
    {
    }

    [FluentMethod(0, "Method1")]
    private void Method4(int p1, out string p2)
    {
        p2 = string.Empty;
    }
}