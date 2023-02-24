// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.InvalidFluentPredicateTypeClass;

[FluentApi]
public class Student
{
    [FluentPredicate(0)]
    public string IsHappy { get; private set; }
}