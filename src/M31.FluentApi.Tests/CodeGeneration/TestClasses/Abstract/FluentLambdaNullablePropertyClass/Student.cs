// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaNullablePropertyClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentLambda(1)]
    [FluentNullable]
    public Address? Address { get; set; }
}

[FluentApi]
public class Address
{
    [FluentMember(0)]
    public string HouseNumber { get; set; }

    [FluentMember(1)]
    public string Street { get; set; }

    [FluentMember(2, "InCity")]
    public string City { get; set; }
}