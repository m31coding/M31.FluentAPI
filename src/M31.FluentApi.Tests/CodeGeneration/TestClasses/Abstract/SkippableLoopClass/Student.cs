// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableLoopClass;

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
    [FluentContinueWith(1)]
    public string? Member3 { get; set; }

    [FluentMember(3)]
    public string Member4 { get; set; }
}