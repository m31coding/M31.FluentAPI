// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using M31.FluentApi.Attributes;
using System;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TryBreakFluentApiClass1;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.ISomeMethod
{
    private readonly Student student;
    private static readonly MethodInfo someMethodMethodInfo;

    static CreateStudent()
    {
        CreateStudent.someMethodMethodInfo = typeof(Student).GetMethod(
            "SomeMethod",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(string) },
            null)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static Student SomeMethod(string someMethodMethodInfo)
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.someMethodMethodInfo.Invoke(createStudent.student, new object?[] { someMethodMethodInfo });
        return createStudent.student;
    }

    Student ISomeMethod.SomeMethod(string someMethodMethodInfo)
    {
        CreateStudent.someMethodMethodInfo.Invoke(student, new object?[] { someMethodMethodInfo });
        return student;
    }

    public interface ICreateStudent : ISomeMethod
    {
    }

    public interface ISomeMethod
    {
        Student SomeMethod(string someMethodMethodInfo);
    }
}