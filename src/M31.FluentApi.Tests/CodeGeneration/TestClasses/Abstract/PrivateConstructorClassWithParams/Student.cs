// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateConstructorClassWithParams;

[FluentApi]
public class Student
{
    private Student(params string[] hobbies)
    {
        Hobbies = hobbies;
    }

    [FluentMember(0, "InSemester")]
    public int Semester { get; set; }

    public string[] Hobbies { get; }
}