// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.ConflictingControlAttributesClass3;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentMethod(1)]
    [FluentBreak]
    [FluentReturn]
    public void Method1()
    {
    }
}