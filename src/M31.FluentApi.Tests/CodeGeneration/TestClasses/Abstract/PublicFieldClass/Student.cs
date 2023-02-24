using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PublicFieldClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    public int Semester;
}