// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.PrivateGetMissingSetClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    private int Semester { get; }
}