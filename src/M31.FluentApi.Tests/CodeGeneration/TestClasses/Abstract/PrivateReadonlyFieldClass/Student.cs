// Field is never used
#pragma warning disable CS0169

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateReadonlyFieldClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    private readonly int semester;
}