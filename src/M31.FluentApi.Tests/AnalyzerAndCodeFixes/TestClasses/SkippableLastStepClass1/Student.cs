// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.SkippableLastStepClass1;

[FluentApi]
public partial class Student
{
    [FluentMember(0)]
    public string FirstName { get; private set; }

    [FluentMember(1)]
    [FluentSkippable]
    public string LastName { get; private set; }
}