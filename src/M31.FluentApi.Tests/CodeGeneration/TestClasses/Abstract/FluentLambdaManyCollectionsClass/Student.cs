// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyCollectionsClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentCollection(1, "AddressA")]
    public List<Address> AddressesA { get; set; }

    [FluentCollection(1, "AddressB")]
    public IReadOnlyCollection<Address> AddressesB { get; set; }

    [FluentCollection(1, "AddressC")]
    public Address[] AddressesC { get; set; }

    [FluentCollection(1, "AddressD")]
    public Address[]? AddressesD { get; set; }

    [FluentCollection(1, "AddressE")]
    public HashSet<Address> AddressesE { get; set; }

    [FluentCollection(1, "AddressF")]
    public Address?[] AddressesF { get; set; }

    [FluentCollection(1, "AddressG")]
    public Address?[]? AddressesG { get; set; }
}