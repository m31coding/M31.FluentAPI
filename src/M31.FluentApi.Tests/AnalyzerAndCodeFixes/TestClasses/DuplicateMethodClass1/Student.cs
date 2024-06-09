// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodClass1;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }

    [FluentMember(1, "WithName")]
    public string LastName { get; set; }
}