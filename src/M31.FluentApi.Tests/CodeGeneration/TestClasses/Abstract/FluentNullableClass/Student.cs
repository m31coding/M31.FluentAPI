// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentNullableClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    [FluentNullable("WhoseNameIsUnknown")]
    public string? Name { get; set; }

    [FluentMember(1, "BornOn")]
    [FluentNullable()]
    public DateOnly? DateOfBirth { get; set; }
}