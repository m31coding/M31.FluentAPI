// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithSelfClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string FirstName { get; private set; }

    [FluentMember(1)]
    [FluentContinueWith(1)]
    public string? MiddleName { get; private set; }

    [FluentMember(1)] public string LastName { get; private set; }
}