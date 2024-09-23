// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassProtectedSetters;

[FluentApi]
public class Student : Person
{
    [FluentMember(0, "WithName")]
    public string Name { get; protected set; }

    [FluentMember(1, "BornOn")]
    public DateOnly DateOfBirth{ get; protected set; }
}