// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodPartialClass;

public partial class Student
{
    [FluentMember(1, "WithName")]
    public string LastName { get; set; }
}