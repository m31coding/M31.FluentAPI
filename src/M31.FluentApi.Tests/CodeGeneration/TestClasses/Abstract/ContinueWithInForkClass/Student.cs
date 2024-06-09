// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithInForkClass;

[FluentApi]
public class Student
{
    [FluentMember(1)]
    public string Member1 { get; set; }

    [FluentMember(2)]
    [FluentContinueWith(3)]
    public string? Member2A { get; set; }

    [FluentMember(2)]
    [FluentContinueWith(4)]
    public string? Member2B { get; set; }

    [FluentMember(3)]
    public string? Member3 { get; set; }

    [FluentMember(4)]
    public string Member4 { get; set; }
}