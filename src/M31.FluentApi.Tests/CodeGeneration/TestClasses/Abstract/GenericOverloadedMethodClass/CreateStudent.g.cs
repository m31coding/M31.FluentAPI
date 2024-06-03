// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericOverloadedMethodClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IMethod1Method1Method1Method1Method1Method1Method1
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static Student Method1(int p1, string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1(p1, p2);
        return createStudent.student;
    }

    public static Student Method1<T>(int p1, string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<T>(p1, p2);
        return createStudent.student;
    }

    public static Student Method1<T>(T p1, string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<T>(p1, p2);
        return createStudent.student;
    }

    public static Student Method1<S, T>(T p1, string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<S, T>(p1, p2);
        return createStudent.student;
    }

    public static Student Method1<S, T>(T p1, out string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<S, T>(p1, out p2);
        return createStudent.student;
    }

    public static Student Method1<S, T>(in T p1, string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<S, T>(in p1, p2);
        return createStudent.student;
    }

    public static Student Method1<S, T>(in T p1, ref string p2)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Method1<S, T>(in p1, ref p2);
        return createStudent.student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1(int p1, string p2)
    {
        student.Method1(p1, p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<T>(int p1, string p2)
    {
        student.Method1<T>(p1, p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<T>(T p1, string p2)
    {
        student.Method1<T>(p1, p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<S, T>(T p1, string p2)
    {
        student.Method1<S, T>(p1, p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<S, T>(T p1, out string p2)
    {
        student.Method1<S, T>(p1, out p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<S, T>(in T p1, string p2)
    {
        student.Method1<S, T>(in p1, p2);
        return student;
    }

    Student IMethod1Method1Method1Method1Method1Method1Method1.Method1<S, T>(in T p1, ref string p2)
    {
        student.Method1<S, T>(in p1, ref p2);
        return student;
    }

    public interface ICreateStudent : IMethod1Method1Method1Method1Method1Method1Method1
    {
    }

    public interface IMethod1Method1Method1Method1Method1Method1Method1
    {
        Student Method1(int p1, string p2);

        Student Method1<T>(int p1, string p2);

        Student Method1<T>(T p1, string p2);

        Student Method1<S, T>(T p1, string p2);

        Student Method1<S, T>(T p1, out string p2);

        Student Method1<S, T>(in T p1, string p2);

        Student Method1<S, T>(in T p1, ref string p2);
    }
}