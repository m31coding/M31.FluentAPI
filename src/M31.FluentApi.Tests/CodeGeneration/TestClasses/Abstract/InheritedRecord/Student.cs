// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedRecord;

[FluentApi]
public record Student(
    string Name,
    DateOnly DateOfBirth,
    [property: FluentMember(2, "InSemester")] int Semester)
    : Person(Name, DateOfBirth);