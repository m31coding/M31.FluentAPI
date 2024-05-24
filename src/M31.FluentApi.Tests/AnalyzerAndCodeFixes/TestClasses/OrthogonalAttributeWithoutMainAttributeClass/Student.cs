// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.OrthogonalAttributeWithoutMainAttributeClass;

[FluentApi]
public class Student
{
    [FluentDefault]
    public int Semester { get; }
}