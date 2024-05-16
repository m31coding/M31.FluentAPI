// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnSingleStepClass;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    [FluentReturn]
    public void ReturnVoidMethod()
    {
        return;
    }

    [FluentMethod(0)]
    [FluentReturn]
    public int ReturnIntMethod()
    {
        return 24;
    }

    [FluentMethod(0)]
    [FluentReturn]
    public List<int> ReturnListMethod()
    {
        return new List<int>() { 1, 2, 3 };
    }

    [FluentMethod(0)]
    [FluentReturn]
    public int ReturnIntMethodWithRefParameter(ref string s)
    {
        return 28;
    }
}