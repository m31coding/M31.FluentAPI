using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.MissingDefaultConstructorClass;

[FluentApi]
public class Student
{
    public Student(int semester)
    {
        Semester = semester;
    }

    [FluentMember(0)]
    public int Semester { get; set; }
}