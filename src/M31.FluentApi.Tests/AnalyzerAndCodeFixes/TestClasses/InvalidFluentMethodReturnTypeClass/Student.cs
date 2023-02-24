// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.InvalidFluentMethodReturnTypeClass;

[FluentApi]
public class Student
{
    public string Name { get; private set; }

    [FluentMethod(0)]
    public int WithName(string name)
    {
        Name = name;
        return 0;
    }
}