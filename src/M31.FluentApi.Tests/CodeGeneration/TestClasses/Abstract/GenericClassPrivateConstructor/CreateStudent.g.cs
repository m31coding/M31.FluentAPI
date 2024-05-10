// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using M31.FluentApi.Attributes;
using System;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassPrivateConstructor;

public class CreateStudent<T1, T2>
{
    private readonly Student<T1, T2> student;

    private CreateStudent()
    {
        student = (Student<T1, T2>) Activator.CreateInstance(typeof(Student<T1, T2>), true)!;
    }

    public static Student<T1, T2> WithProperty1(T1 property1)
    {
        CreateStudent<T1, T2> createStudent = new CreateStudent<T1, T2>();
        createStudent.student.Property1 = property1;
        return createStudent.student;
    }

    public static Student<T1, T2> WithProperty2(T2 property2)
    {
        CreateStudent<T1, T2> createStudent = new CreateStudent<T1, T2>();
        createStudent.student.Property2 = property2;
        return createStudent.student;
    }
}