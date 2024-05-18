// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithConstraints;

public class CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9> :
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.ICreateStudent,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty1,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty2,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty3,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty4,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty5,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty6,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty7,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty8,
    CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>.IWithProperty9
    where T1 : class
    where T2 : class?
    where T3 : struct
    where T4 : notnull
    where T5 : new()
    where T6 : unmanaged
    where T7 : System.Collections.Generic.List<int>, System.Collections.Generic.IDictionary<int, string>
    where T8 : class, System.Collections.Generic.IDictionary<int, string>
    where T9 : System.Collections.Generic.List<int>, new()
{
    private readonly Student<T1, T2, T3, T4, T5, T6, T7, T8, T9> student;

    private CreateStudent()
    {
        student = new Student<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
    }

    public static IWithProperty2 WithProperty1(T1 property1)
    {
        CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9> createStudent = new CreateStudent<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        createStudent.student.Property1 = property1;
        return createStudent;
    }

    IWithProperty2 IWithProperty1.WithProperty1(T1 property1)
    {
        student.Property1 = property1;
        return this;
    }

    IWithProperty3 IWithProperty2.WithProperty2(T2 property2)
    {
        student.Property2 = property2;
        return this;
    }

    IWithProperty4 IWithProperty3.WithProperty3(T3 property3)
    {
        student.Property3 = property3;
        return this;
    }

    IWithProperty5 IWithProperty4.WithProperty4(T4 property4)
    {
        student.Property4 = property4;
        return this;
    }

    IWithProperty6 IWithProperty5.WithProperty5(T5 property5)
    {
        student.Property5 = property5;
        return this;
    }

    IWithProperty7 IWithProperty6.WithProperty6(T6 property6)
    {
        student.Property6 = property6;
        return this;
    }

    IWithProperty8 IWithProperty7.WithProperty7(T7 property7)
    {
        student.Property7 = property7;
        return this;
    }

    IWithProperty9 IWithProperty8.WithProperty8(T8 property8)
    {
        student.Property8 = property8;
        return this;
    }

    Student<T1, T2, T3, T4, T5, T6, T7, T8, T9> IWithProperty9.WithProperty9(T9 property9)
    {
        student.Property9 = property9;
        return student;
    }

    public interface ICreateStudent : IWithProperty1
    {
    }

    public interface IWithProperty1
    {
        IWithProperty2 WithProperty1(T1 property1);
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
        IWithProperty6 WithProperty5(T5 property5);
    }

    public interface IWithProperty6
    {
        IWithProperty7 WithProperty6(T6 property6);
    }

    public interface IWithProperty7
    {
        IWithProperty8 WithProperty7(T7 property7);
    }

    public interface IWithProperty8
    {
        IWithProperty9 WithProperty8(T8 property8);
    }

    public interface IWithProperty9
    {
        Student<T1, T2, T3, T4, T5, T6, T7, T8, T9> WithProperty9(T9 property9);
    }
}