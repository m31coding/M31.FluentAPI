// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    [FluentCollection(1, "Address")]
    public List<Address> Addresses { get; set; }
}