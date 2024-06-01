// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.GetMissingSetRecordStruct;

[FluentApi]
public record struct Student
{
    [FluentMember(0)]
    int Semester { get; }
}