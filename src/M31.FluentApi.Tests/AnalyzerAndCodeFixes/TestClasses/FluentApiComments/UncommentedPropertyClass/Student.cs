// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.FluentApiComments.UncommentedPropertyClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string FirstName { get; set; }
}