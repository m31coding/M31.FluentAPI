// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentMethod(1)]
    [FluentReturn]
    private void ReturnVoidMethod()
    {
        return;
    }

    [FluentMethod(1)]
    [FluentReturn]
    private int ReturnIntMethod()
    {
        return 24;
    }

    [FluentMethod(1)]
    [FluentReturn]
    private List<int> ReturnListMethod()
    {
        return new List<int>() { 1, 2, 3 };
    }

    [FluentMethod(1)]
    [FluentReturn]
    private int ReturnIntMethodWithRefParameter(ref string s)
    {
        return 28;
    }
}