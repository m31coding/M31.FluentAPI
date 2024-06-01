// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.PartialClass;

public partial class Student
{
    [FluentMember(1)]
    public string LastName { get; private set; }
}