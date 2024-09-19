// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

// Example from https://youtu.be/qCIr30WxJQw?si=FRALafrpA1zWACA8.
// Implementation with arbitrary steps.

using M31.FluentApi.Attributes;

namespace ExampleProject;

public class BaseOrder
{
    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public virtual DateTime? CreatedOn { get; protected set; }
}

[FluentApi]
public class Order3 : BaseOrder
{
    [FluentMember(0)]
    [FluentContinueWith(0)]
    public int? Number { get; private set; }
    
    [FluentMember(0, "ShippedTo")]
    [FluentContinueWith(0)]
    public Address3? ShippingAddress { get; private set; }

    [FluentMethod(0)]
    private void Build()
    {
    }
}

[FluentApi]
public class Address3
{
    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public string Street { get; private set; }

    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public string City { get; private set; }

    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public string Zip { get; private set; }

    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public string State { get; private set; }

    [FluentMember(0, "{Name}")]
    [FluentContinueWith(0)]
    public string Country { get; private set; }

    [FluentMethod(0)]
    private void Build()
    {
        Street ??= "N/A";
        City ??= "N/A";
        Zip ??= "N/A";
        State ??= "N/A";
        Country ??= "N/A";
    }
}