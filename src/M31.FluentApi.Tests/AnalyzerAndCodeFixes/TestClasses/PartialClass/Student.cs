using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.PartialClass;

[FluentApi]
public partial class Student
{
    [FluentMember(0)]
    public int Semester { get; private set; }
}