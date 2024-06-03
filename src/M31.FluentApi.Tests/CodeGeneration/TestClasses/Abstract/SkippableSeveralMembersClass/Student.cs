// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableSeveralMembersClass;

[FluentApi]
public class Student
{
    [FluentMember(1)]
    [FluentSkippable]
    public string? Member1 { get; set; }

    [FluentMember(2)]
    [FluentSkippable]
    public string? Member2 { get; set; }

    [FluentMember(3)]
    [FluentSkippable]
    public string? Member3 { get; set; }

    [FluentMember(4)]
    public string Member4 { get; set; }
}