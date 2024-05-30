// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.OrthogonalAttributeInCompoundClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "Named", 0)]
    public string FirstName { get; private set; }

    [FluentMember(0, "Named", 1)]
    [FluentDefault]
    public string LastName { get; private set; }
}