// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableTwoLoopsClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentSkippable]
    public string? Member0 { get; set; }

    [FluentMember(1)]
    [FluentSkippable]
    [FluentContinueWith(0)]
    public string? Member1 { get; set; }

    [FluentMember(1)]
    public string? Member1B { get; set; }

    [FluentMember(2)]
    public string? Member2 { get; set; }

    [FluentMember(3)]
    [FluentSkippable]
    public string? Member3 { get; set; }

    [FluentMember(4)]
    [FluentSkippable]
    [FluentContinueWith(3)]
    public string? Member4 { get; set; }

    [FluentMember(4)]
    public string? Member4B { get; set; }

    [FluentMember(5)]
    public string? Member5 { get; set; }
}