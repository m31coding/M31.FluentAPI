// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.ConflictingControlAttributesClass1;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentBreak]
    [FluentContinueWith(1)]
    public string Name { get; set; }

    [FluentMember(1, "InSemester")]
    public int Semester { get; private set; }
}