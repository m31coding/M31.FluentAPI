// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DefaultFluentMethodNameClass;

[FluentApi]
public class Student
{
    public string Name { get; set; }

    [FluentPredicate(1)]
    public bool IsHappy { get; set; }

    [FluentMember(2)]
    public int Semester { get; set; }

    [FluentCollection(3, "Friend")]
    public IReadOnlyCollection<string> Friends { get; set; }

    [FluentMethod(0)]
    public void WithName(string firstName, string lastName)
    {
        Name = $"{lastName}, {firstName}";
    }
}