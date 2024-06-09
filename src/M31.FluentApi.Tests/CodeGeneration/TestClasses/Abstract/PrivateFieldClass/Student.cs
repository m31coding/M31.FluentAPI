// Field is never assigned to, and will always have its default value
#pragma warning disable CS0649
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFieldClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    private int semester;

    public int Semester => semester;
}