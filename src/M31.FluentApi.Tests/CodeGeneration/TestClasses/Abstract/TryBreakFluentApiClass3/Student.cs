// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TryBreakFluentApiClass3;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithDetails")]
    public string CreateAddress { get; set; }

    [FluentMember(0, "WithDetails")]
    public Address Address { get; set; }
}