using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.PublicGetMissingSetClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public int Semester { get; }
}