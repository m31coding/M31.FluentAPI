// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.OverloadedMethodClass;

[FluentApi]
public class Student
{
    public string? FirstName { get; private set; }
    public string LastName { get; private set; }

    [FluentMethod(0)]
    private void Named(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [FluentMethod(0)]
    private void Named(string lastName)
    {
        LastName = lastName;
    }
}