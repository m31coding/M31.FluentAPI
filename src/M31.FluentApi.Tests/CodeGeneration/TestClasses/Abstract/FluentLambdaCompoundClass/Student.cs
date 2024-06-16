// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCompoundClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentMember(1, "WithDetails")]
    public Address Address { get; set; }

    [FluentMember(1, "WithDetails")]
    public Phone Phone { get; set; }
}