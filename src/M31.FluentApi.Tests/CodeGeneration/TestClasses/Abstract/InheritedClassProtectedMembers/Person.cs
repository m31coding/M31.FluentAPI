// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassProtectedMembers;

[FluentApi]
public class Person
{
    [FluentMember(2, "InSemester")]
    protected int Semester { get; set; }
}