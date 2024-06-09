// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentDefaultMemberClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    [FluentDefault("WithUnknownName")]
    public string Name { get; set; } = "unknown";

    [FluentMember(1, "BornOn")]
    [FluentDefault]
    public DateOnly DateOfBirth { get; set; } = new DateOnly();
}