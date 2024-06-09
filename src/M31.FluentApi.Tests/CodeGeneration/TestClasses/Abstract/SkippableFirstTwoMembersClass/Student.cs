// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableFirstTwoMembersClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentSkippable]
    public string? FirstName { get; set; }

    [FluentMember(1)]
    [FluentSkippable]
    public string? MiddleName { get; set; }

    [FluentMember(2)]
    public string LastName { get; set; }
}