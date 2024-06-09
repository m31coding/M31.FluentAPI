// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithConstraints;

[FluentApi]
public class Student<T1, T2, T3, T4, T5, T6, T7, T8, T9>
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

    [FluentMember(6)]
    public T6 Property6 { get; set; }

    [FluentMember(7)]
    public T7 Property7 { get; set; }

    [FluentMember(8)]
    public T8 Property8 { get; set; }

    [FluentMember(9)]
    public T9 Property9 { get; set; }
}