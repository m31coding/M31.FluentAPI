// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using M31.FluentApi.Attributes;
using System;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFluentMethodParameterModifiersClass;

public class CreateStudent :
    CreateStudent.IMethodWithRefParameter,
    CreateStudent.IMethodWithInParameter,
    CreateStudent.IMethodWithOutParameter,
    CreateStudent.IMethodWithRefInAndOutParameter
{
    private readonly Student student;
    private static readonly MethodInfo methodWithParamsMethodInfo;
    private static readonly MethodInfo methodWithRefParameterMethodInfo;
    private static readonly MethodInfo methodWithInParameterMethodInfo;
    private static readonly MethodInfo methodWithOutParameterMethodInfo;
    private static readonly MethodInfo methodWithRefInAndOutParameterMethodInfo;

    static CreateStudent()
    {
        methodWithParamsMethodInfo = typeof(Student).GetMethod(
            "MethodWithParams",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int[]) },
            null)!;
        methodWithRefParameterMethodInfo = typeof(Student).GetMethod(
            "MethodWithRefParameter",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int).MakeByRefType() },
            null)!;
        methodWithInParameterMethodInfo = typeof(Student).GetMethod(
            "MethodWithInParameter",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int).MakeByRefType() },
            null)!;
        methodWithOutParameterMethodInfo = typeof(Student).GetMethod(
            "MethodWithOutParameter",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int).MakeByRefType() },
            null)!;
        methodWithRefInAndOutParameterMethodInfo = typeof(Student).GetMethod(
            "MethodWithRefInAndOutParameter",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() },
            null)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static IMethodWithRefParameter MethodWithParams(params int[] numbers)
    {
        CreateStudent createStudent = new CreateStudent();
        methodWithParamsMethodInfo.Invoke(createStudent.student, new object?[] { numbers });
        return createStudent;
    }

    public IMethodWithInParameter MethodWithRefParameter(ref int n1)
    {
        object?[] args = new object?[] { n1 };
        methodWithRefParameterMethodInfo.Invoke(student, args);
        n1 = (int) args[0]!;
        return this;
    }

    public IMethodWithOutParameter MethodWithInParameter(in int n2)
    {
        methodWithInParameterMethodInfo.Invoke(student, new object?[] { n2 });
        return this;
    }

    public IMethodWithRefInAndOutParameter MethodWithOutParameter(out int n3)
    {
        object?[] args = new object?[] { null };
        methodWithOutParameterMethodInfo.Invoke(student, args);
        n3 = (int) args[0]!;
        return this;
    }

    public Student MethodWithRefInAndOutParameter(ref int n4, in int n5, out int n6)
    {
        object?[] args = new object?[] { n4, n5, null };
        methodWithRefInAndOutParameterMethodInfo.Invoke(student, args);
        n4 = (int) args[0]!;
        n6 = (int) args[2]!;
        return student;
    }

    public interface IMethodWithRefParameter
    {
        IMethodWithInParameter MethodWithRefParameter(ref int n1);
    }

    public interface IMethodWithInParameter
    {
        IMethodWithOutParameter MethodWithInParameter(in int n2);
    }

    public interface IMethodWithOutParameter
    {
        IMethodWithRefInAndOutParameter MethodWithOutParameter(out int n3);
    }

    public interface IMethodWithRefInAndOutParameter
    {
        Student MethodWithRefInAndOutParameter(ref int n4, in int n5, out int n6);
    }
}