// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.GetMissingSetRecord;

[FluentApi]
public record Student
{
    [FluentMember(0)]
    int Semester { get; }
}