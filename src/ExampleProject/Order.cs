// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

// Example from https://youtu.be/qCIr30WxJQw?si=FRALafrpA1zWACA8.
// Implementation with forced steps.

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class Order
{
    [FluentMember(0)]
    public int Number { get; private set; }

    [FluentMember(1, "{Name}")]
    public DateTime CreatedOn { get; private set; }

    [FluentLambda(2, "ShippedTo")]
    public Address ShippingAddress { get; private set; }
}

[FluentApi]
public class Address
{
    [FluentMember(0, "{Name}")]
    public string Street { get; private set; }

    [FluentMember(1, "{Name}")]
    public string City { get; private set; }

    [FluentMember(2, "{Name}")]
    public string Zip { get; private set; }

    [FluentMember(3, "{Name}")]
    [FluentDefault("Default{Name}")]
    public string State { get; private set; } = "N/A";

    [FluentMember(4, "{Name}")]
    public string Country { get; private set; }
}