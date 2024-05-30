// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnMultiStepClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentMethod(1)]
    [FluentReturn]
    public void ReturnVoidMethod()
    {
        return;
    }

    [FluentMethod(1)]
    [FluentReturn]
    public int ReturnIntMethod()
    {
        return 24;
    }

    [FluentMethod(1)]
    [FluentReturn]
    public List<int> ReturnListMethod()
    {
        return new List<int>() { 1, 2, 3 };
    }

    [FluentMethod(1)]
    [FluentReturn]
    public int ReturnIntMethodWithRefParameter(ref string s)
    {
        return 28;
    }
}