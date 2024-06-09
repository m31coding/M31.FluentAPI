// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableForkMembersClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentSkippable]
    public string? Member0 { get; set; }

    [FluentMember(1)]
    [FluentSkippable]
    public string? Member1A { get; set; }

    [FluentMember(1)]
    [FluentSkippable]
    [FluentContinueWith(0)]
    public string? Member1B { get; set; }

    [FluentMember(2)]
    public string? Member2 { get; set; }
}