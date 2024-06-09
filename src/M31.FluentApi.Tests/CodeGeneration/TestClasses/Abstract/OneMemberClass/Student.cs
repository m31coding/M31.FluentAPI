// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.OneMemberClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    public int Semester { get; set; }
}