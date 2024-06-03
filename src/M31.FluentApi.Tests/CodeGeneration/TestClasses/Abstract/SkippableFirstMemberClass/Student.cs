// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableFirstMemberClass;

[FluentApi]
public class Student
{
    [FluentSkippable]
    [FluentMember(0)]
    public string? FirstName { get; set; }

    [FluentMember(1)]
    public string LastName { get; set; }
}