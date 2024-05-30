// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.ConflictingControlAttributesClass2;

[FluentApi]
public class Student
{
    [FluentMember(0, "Named", 0)]
    [FluentContinueWith(1)]
    public string FirstName { get; private set; }

    [FluentMember(0, "Named", 1)]
    [FluentContinueWith(0)]
    public string LastName { get; private set; }

    [FluentMember(1, "InSemester")]
    public int Semester { get; private set; }
}