// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.ReservedMethodClass1;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    public void InitialStep()
    {
    }

    [FluentMethod(1, "InitialStep")]
    public void Method1()
    {
    }
}