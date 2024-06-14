// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCompoundOfSameTypeClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentMember(1, "WithDetails")]
    public Address Address1 { get; set; }

    [FluentMember(1, "WithDetails")]
    public Address Address2 { get; set; }
}