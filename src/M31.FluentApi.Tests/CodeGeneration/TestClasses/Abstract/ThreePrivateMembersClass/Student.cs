// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ThreePrivateMembersClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    public string Name { get; private set; }

    [FluentMember(1, "BornOn")]
    public DateOnly DateOfBirth{ get; private set; }

    [FluentMember(2, "InSemester")]
    public int Semester { get; private set; }
}