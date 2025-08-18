// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FullyQualifiedTypeClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "BornOn")]
    public System.DateOnly DateOfBirth { get; set; }

    [FluentCollection(0, "Friend")]
    public System.Collections.Generic.List<string> Friends { get; set; }
}