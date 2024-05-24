// Non-nullable member is uninitialized
#pragma warning disable CS8618
// Field is never used
#pragma warning disable CS0169
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SameNameMemberClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    public int Semester { get; private set; }

    [FluentMember(1, "WithName")]
    public char Initial { get; private set; }

    [FluentMember(2, "WithName")]
    public string LastName { get; private set; }
}