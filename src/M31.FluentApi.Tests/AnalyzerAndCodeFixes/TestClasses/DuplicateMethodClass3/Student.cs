// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodClass3;

[FluentApi]
public class Student
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    [FluentMethod(0, "WithName")]
    public void WithFirstName(string firstName)
    {
        FirstName = firstName;
    }

    [FluentMethod(1, "WithName")]
    public void WithLastName(string lastName)
    {
        LastName = lastName;
    }
}