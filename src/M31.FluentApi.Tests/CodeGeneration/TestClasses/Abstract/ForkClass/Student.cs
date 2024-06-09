// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ForkClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "OfAge")]
    public int Age { get; set; }

    [FluentMember(0, "BornOn")]
    public DateOnly DateOfBirth { get; set; }
}