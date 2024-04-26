// Non-nullable member is uninitialized

#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericMethodWithConstraintsClass;

[FluentApi]
public class Student
{
    [FluentMethod(1)]
    public void Method1<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        T1 p1,
        T2 p2,
        T3 p3,
        T4 p4,
        T5 p5,
        T6 p6,
        T7 p7,
        T8 p8,
        T9 p9)
        where T1 : class
        where T2 : class?
        where T3 : struct
        where T4 : notnull
        where T5 : new()
        where T6 : unmanaged
        where T7 : List<int>, IDictionary<int, string>
        where T8 : class, IDictionary<int, string>
        where T9 : List<int>, new()
    {
    }
}