// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class ExchangeStudent : Student
{
    [FluentMember(6)]
    public string HomeCountry { get; private set; }
}