// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InternalClass;

[FluentApi]
internal class Student
{
    [FluentMember(0, "WithName")]
    public string Name { get; set; }

    [FluentMember(1, "BornOn")]
    public DateOnly DateOfBirth{ get; set; }

    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }
}