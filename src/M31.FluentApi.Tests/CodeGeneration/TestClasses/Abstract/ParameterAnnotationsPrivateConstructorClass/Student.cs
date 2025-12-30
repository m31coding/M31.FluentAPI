// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ParameterAnnotationsPrivateConstructorClass;

[FluentApi]
public class Student
{
    private Student(ref string property1, in int property2, out double outValue)
    {
        Property1 = property1;
        Property2 = property2;
        outValue = 1;
    }

    [FluentMember(0)]
    public string Property1 { get; set; }

    [FluentMember(1)]
    public int Property2 { get; set; }
}