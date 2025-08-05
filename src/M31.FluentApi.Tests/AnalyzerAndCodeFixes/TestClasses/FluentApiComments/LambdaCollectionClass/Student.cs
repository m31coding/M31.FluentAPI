// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.FluentApiComments.LambdaCollectionClass;

[FluentApi]
public class Student
{
    [FluentMember(0)] public string Name { get; set; }

    [FluentCollection(1, "PhoneNumber")]
    public IReadOnlyCollection<Phone> PhoneNumbers { get; set; }
}

[FluentApi]
public class Phone
{
    [FluentMember(0)]
    public string Number { get; set; }

    [FluentMember(1)]
    public string Usage { get; set; }
}