// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentCollection(1, "AddressA")]
    public List<Address> AddressesA { get; private set; }

    [FluentCollection(1, "AddressB")]
    public IReadOnlyCollection<Address> AddressesB { get; private set; }

    [FluentCollection(1, "AddressC")]
    public Address[] AddressesC { get; private set; }

    [FluentCollection(1, "AddressD")]
    public HashSet<Address> AddressesD { get; private set; }

    [FluentCollection(1, "AddressE")]
    public Address[]? AddressesE { get; private set; }

    [FluentCollection(1, "AddressF")]
    public Address?[] AddressesF { get; private set; }

    [FluentCollection(1, "AddressG")]
    public Address?[]? AddressesG { get; private set; }
}