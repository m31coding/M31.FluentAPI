// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    private void Method1(int p1, string p2)
    {

    }

    [FluentMethod(0)]
    private void Method1<T>(int p1, string p2)
    {

    }

    [FluentMethod(0)]
    private void Method1<T>(T p1, string p2)
    {

    }

    [FluentMethod(0)]
    private void Method1<S, T>(T p1, string p2)
    {

    }

    [FluentMethod(0)]
    private void Method1<S, T>(T p1, out string p2)
    {
        p2 = string.Empty;
    }

    [FluentMethod(0)]
    private void Method1<S, T>(in T p1, string p2)
    {

    }

    [FluentMethod(0)]
    private void Method1<S, T>(in T p1, ref string p2)
    {

    }
}