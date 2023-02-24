using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.InvalidNullableTypeClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentNullable]
    public int Semester { get; set; }
}