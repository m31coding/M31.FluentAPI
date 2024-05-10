// Non-nullable member is uninitialized

#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithOverloadedGenericMethod;

[FluentApi]
public class Student
{
    public List<string> Logs { get; } = new List<string>();

    [FluentMethod(0)]
    public void Method1(int p1, string p2)
    {
        Logs.Add("Called Method1(int, string)");
    }

    [FluentMethod(0)]
    public void Method1<T>(int p1, string p2)
    {
        Logs.Add("Called Method1<T>(int, string)");
    }

    [FluentMethod(0)]
    public void Method1<T>(T p1, string p2)
    {
        Logs.Add("Called Method1<T>(T, string)");
    }

    [FluentMethod(0)]
    public void Method1<S, T>(T p1, string p2)
    {
        Logs.Add("Called Method1<S, T>(T, string)");
    }

    [FluentMethod(0)]
    public void Method1<S, T>(T p1, out string p2)
    {
        p2 = string.Empty;
        Logs.Add("Called Method1<S, T>(T, out string)");
    }

    [FluentMethod(0)]
    public void Method1<S, T>(in T p1, string p2)
    {
        Logs.Add("Called Method1<S, T>(in T, string)");
    }

    [FluentMethod(0)]
    public void Method1<S, T>(in T p1, ref string p2)
    {
        Logs.Add("Called Method1<S, T>(in T, ref string)");
    }
}