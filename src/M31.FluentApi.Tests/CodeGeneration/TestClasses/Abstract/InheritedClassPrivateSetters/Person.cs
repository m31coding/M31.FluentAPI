// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassPrivateSetters;

[FluentApi]
public class Person
{
    [FluentMember(0, "WithName")]
    public string Name { get; private set; }

    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = new DateOnly(2024, 9, 26);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }
}