// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.AmbiguousConstructorsClass;

[FluentApi]
public class Student
{
    public Student(int semester)
    {
        Semester = semester;
    }

    public Student(string semester)
    {
        Semester = int.Parse(semester);
    }

    [FluentMember(0)]
    public int Semester { get; set; }
}