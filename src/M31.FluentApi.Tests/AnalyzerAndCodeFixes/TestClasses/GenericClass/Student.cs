// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.GenericClass;

[FluentApi]
public class Student<T>
{
    [FluentMember(0)]
    public T Item { get; set; }
}