// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassProtectedMembers;

[FluentApi]
public class Student : Person
{
    [FluentMember(0, "WithName")]
    protected string Name { get; set; }

    [FluentMember(1, "BornOn")]
    protected DateOnly DateOfBirth{ get; set; }
}