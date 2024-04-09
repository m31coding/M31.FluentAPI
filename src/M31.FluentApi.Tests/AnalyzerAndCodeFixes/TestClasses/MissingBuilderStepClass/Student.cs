// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.MissingBuilderStepClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentContinueWith(99)]
    public string FirstName { get; set; }

    [FluentMember(1)]
    public string LastName { get; set; }
}