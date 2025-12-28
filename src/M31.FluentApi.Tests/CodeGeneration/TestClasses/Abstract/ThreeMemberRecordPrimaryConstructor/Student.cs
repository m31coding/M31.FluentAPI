// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ThreeMemberRecordPrimaryConstructor;

[FluentApi]
public record Student(
    [property: FluentMember(0, "WithName")] string name,
    [property: FluentMember(1, "BornOn")] DateOnly dateOfBirth,
    [property: FluentMember(2, "InSemester")] int semester)
{
}