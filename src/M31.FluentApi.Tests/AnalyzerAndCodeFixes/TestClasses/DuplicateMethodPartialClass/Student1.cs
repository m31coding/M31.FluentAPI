// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodPartialClass;

[FluentApi]
public partial class Student
{
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }
}