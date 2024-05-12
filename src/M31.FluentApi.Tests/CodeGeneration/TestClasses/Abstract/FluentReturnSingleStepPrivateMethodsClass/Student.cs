// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    [FluentReturn]
    private void ReturnVoid()
    {
        return;
    }

    [FluentMethod(0)]
    [FluentReturn]
    private int ReturnInt()
    {
        return 24;
    }

    [FluentMethod(0)]
    [FluentReturn]
    private List<int> ReturnList()
    {
        return new List<int>() { 1, 2, 3 };
    }
}