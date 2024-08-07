// Non-nullable member is uninitialized
#pragma warning disable CS8618
// Type or member is obsolete
#pragma warning disable CS0618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.FluentLambdaMemberWithoutFluentApiClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentLambda(1)]
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }

    public string HouseNumber { get; set; }
}