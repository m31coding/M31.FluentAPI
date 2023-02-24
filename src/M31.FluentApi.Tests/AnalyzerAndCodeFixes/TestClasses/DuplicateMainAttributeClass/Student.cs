using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMainAttributeClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentPredicate(0)]
    public bool IsHappy { get; private set; }
}