using M31.FluentApi.Attributes;
// ReSharper disable All

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GetPrivateSetPropertyClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    public int Semester { get; private set; }
}