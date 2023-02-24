// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.NullableTypeNoNullableAnnotationClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    [FluentNullable]
    public string Name { get; set; }
}