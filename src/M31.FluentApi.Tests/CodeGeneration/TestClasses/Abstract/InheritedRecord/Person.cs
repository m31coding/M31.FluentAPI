// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedRecord;

[FluentApi]
public record Person(
    [property: FluentMember(0, "WithName")] string Name,
    [property: FluentMember(1, "BornOn")] DateOnly DateOfBirth);