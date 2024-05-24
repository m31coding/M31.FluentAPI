// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithGenericMethods;

[FluentApi]
public class Student<T1, T2, T3, T4, T5>
    where T1 : class
    where T2 : class?
    where T3 : struct
    where T4 : notnull
    where T5 : new()
{
    public List<string> Logs { get; } = new List<string>();

    [FluentMember(1)]
    public T1 Property1 { get; set; }

    [FluentMember(2)]
    public T2 Property2 { get; set; }

    [FluentMember(3)]
    public T3 Property3 { get; set; }

    [FluentMember(4)]
    public T4 Property4 { get; set; }

    [FluentMember(5)]
    public T5 Property5 { get; set; }

    [FluentMethod(6)]
    public void Method1<T6, T7, T8, T9>(T6 p1, T7 p2, T8 p3, T9 p4)
        where T6 : unmanaged
        where T7 : List<int>, IDictionary<int, string>
        where T8 : class, IDictionary<int, string>
        where T9 : List<int>, new()
    {
        Logs.Add("Called Method1");
    }

    [FluentMethod(7)]
    public void Method2<T6, T7, T8, T9>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9)
        where T6 : unmanaged
        where T7 : List<int>, IDictionary<int, string>
        where T8 : class, IDictionary<int, string>
        where T9 : List<int>, new()
    {
        Logs.Add("Called Method2");
    }

    [FluentMethod(8)]
    public void Method3<T6, T7, T8, T9>(T1 p1)
        where T6 : unmanaged
        where T7 : List<int>, IDictionary<int, string>
        where T8 : class, IDictionary<int, string>
        where T9 : List<int>, new()
    {
        Logs.Add("Called Method3");
    }
}