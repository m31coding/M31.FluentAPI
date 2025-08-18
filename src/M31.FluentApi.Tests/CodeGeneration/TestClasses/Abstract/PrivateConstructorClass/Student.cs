// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateConstructorClass;

[FluentApi]
public class Student
{
    private Student()
    {
    }

    [FluentMember(0, "InSemester")]
    public int Semester { get; set; }
}