// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;
using System;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithPrivateGenericMethods;

public class CreateStudent<T1, T2, T3, T4, T5> :
    CreateStudent<T1, T2, T3, T4, T5>.IWithProperty2,
    CreateStudent<T1, T2, T3, T4, T5>.IWithProperty3,
    CreateStudent<T1, T2, T3, T4, T5>.IWithProperty4,
    CreateStudent<T1, T2, T3, T4, T5>.IWithProperty5,
    CreateStudent<T1, T2, T3, T4, T5>.IMethod1Method2
    where T1 : class
    where T2 : class?
    where T3 : struct
    where T4 : notnull
    where T5 : new()
{
    private readonly Student<T1, T2, T3, T4, T5> student;
    private static readonly MethodInfo method1MethodInfo;
    private static readonly MethodInfo method2MethodInfo;

    static CreateStudent()
    {
        method1MethodInfo = typeof(Student<T1, T2, T3, T4, T5>).GetMethod(
            "Method1",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(1), Type.MakeGenericMethodParameter(2), Type.MakeGenericMethodParameter(3) })!;
        method2MethodInfo = typeof(Student<T1, T2, T3, T4, T5>).GetMethod(
            "Method2",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(1), Type.MakeGenericMethodParameter(2), Type.MakeGenericMethodParameter(3) })!;
    }

    private CreateStudent()
    {
        student = new Student<T1, T2, T3, T4, T5>();
    }

    public static IWithProperty2 WithProperty1(T1 property1)
    {
        CreateStudent<T1, T2, T3, T4, T5> createStudent = new CreateStudent<T1, T2, T3, T4, T5>();
        createStudent.student.Property1 = property1;
        return createStudent;
    }

    public IWithProperty3 WithProperty2(T2 property2)
    {
        student.Property2 = property2;
        return this;
    }

    public IWithProperty4 WithProperty3(T3 property3)
    {
        student.Property3 = property3;
        return this;
    }

    public IWithProperty5 WithProperty4(T4 property4)
    {
        student.Property4 = property4;
        return this;
    }

    public IMethod1Method2 WithProperty5(T5 property5)
    {
        student.Property5 = property5;
        return this;
    }

    public Student<T1, T2, T3, T4, T5> Method1<T6, T7, T8, T9>(T6 p1, T7 p2, T8 p3, T9 p4)
        where T6 : unmanaged
        where T7 : System.Collections.Generic.List<int>, System.Collections.Generic.IDictionary<int, string>
        where T8 : class, System.Collections.Generic.IDictionary<int, string>
        where T9 : System.Collections.Generic.List<int>, new()
    {
        method1MethodInfo.Invoke(student, new object?[] { p1, p2, p3, p4 });
        return student;
    }

    public Student<T1, T2, T3, T4, T5> Method2<T6, T7, T8, T9>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9)
        where T6 : unmanaged
        where T7 : System.Collections.Generic.List<int>, System.Collections.Generic.IDictionary<int, string>
        where T8 : class, System.Collections.Generic.IDictionary<int, string>
        where T9 : System.Collections.Generic.List<int>, new()
    {
        method2MethodInfo.Invoke(student, new object?[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 });
        return student;
    }

    public interface IWithProperty2
    {
        IWithProperty3 WithProperty2(T2 property2);
    }

    public interface IWithProperty3
    {
        IWithProperty4 WithProperty3(T3 property3);
    }

    public interface IWithProperty4
    {
        IWithProperty5 WithProperty4(T4 property4);
    }

    public interface IWithProperty5
    {
        IMethod1Method2 WithProperty5(T5 property5);
    }

    public interface IMethod1Method2
    {
        Student<T1, T2, T3, T4, T5> Method1<T6, T7, T8, T9>(T6 p1, T7 p2, T8 p3, T9 p4)
            where T6 : unmanaged
            where T7 : System.Collections.Generic.List<int>, System.Collections.Generic.IDictionary<int, string>
            where T8 : class, System.Collections.Generic.IDictionary<int, string>
            where T9 : System.Collections.Generic.List<int>, new();
        Student<T1, T2, T3, T4, T5> Method2<T6, T7, T8, T9>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9)
            where T6 : unmanaged
            where T7 : System.Collections.Generic.List<int>, System.Collections.Generic.IDictionary<int, string>
            where T8 : class, System.Collections.Generic.IDictionary<int, string>
            where T9 : System.Collections.Generic.List<int>, new();
    }
}