// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentMethodParameterModifiersClass;

[FluentApi]
public class Student
{
    public int[] Numbers { get; set; }
    public int N1 { get; set; }
    public int N2 { get; set; }
    public int N3 { get; set; }

    [FluentMethod(0)]
    public void MethodWithParams(params int[] numbers)
    {
        Numbers = numbers;
    }

    [FluentMethod(1)]
    public void MethodWithRefParameter(ref int n1)
    {
        n1 = 3;
        N1 = n1;
    }

    [FluentMethod(2)]
    public void MethodWithInParameter(in int n2)
    {
        N2 = n2;
    }

    [FluentMethod(3)]
    public void MethodWithOutParameter(out int n3)
    {
        n3 = 5;
        N3 = n3;
    }
}