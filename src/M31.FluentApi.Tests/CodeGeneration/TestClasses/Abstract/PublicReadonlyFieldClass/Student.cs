using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PublicReadonlyFieldClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    // ReSharper disable once UnassignedReadonlyField
    public readonly int Semester;
}