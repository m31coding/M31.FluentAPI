// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.InvalidCollectionTypeClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend")]
    public string Friends { get; private set; }
}