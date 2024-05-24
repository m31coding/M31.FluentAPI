// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodClass5;

[FluentApi]
public class Student
{
    [FluentMethod(0, "Method1")]
    private void Method2<S>(S p1)
    {
    }

    [FluentMethod(0, "Method1")]
    private void Method3<T>(T p1)
    {
    }
}