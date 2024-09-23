// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClass;

[FluentApi]
public class Person
{
    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }
}