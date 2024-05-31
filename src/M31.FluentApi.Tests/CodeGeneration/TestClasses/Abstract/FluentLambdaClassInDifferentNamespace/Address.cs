// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable CheckNamespace

using M31.FluentApi.Attributes;

namespace SomeOtherNamespace;

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