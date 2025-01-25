// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFluentMethodNullableParameterClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IBornOn,
    CreateStudent.IInSemester
{
    private readonly Student student;
    private static readonly MethodInfo withNameMethodInfo;
    private static readonly MethodInfo bornOnMethodInfo;
    private static readonly MethodInfo inSemesterMethodInfo;

    static CreateStudent()
    {
        withNameMethodInfo = typeof(Student).GetMethod(
            "WithName",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(string) },
            null)!;
        bornOnMethodInfo = typeof(Student).GetMethod(
            "BornOn",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(System.DateOnly?) },
            null)!;
        inSemesterMethodInfo = typeof(Student).GetMethod(
            "InSemester",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(int?) },
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

    public static IBornOn WithName(string? name)
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.withNameMethodInfo.Invoke(createStudent.student, new object?[] { name });
        return createStudent;
    }

    IBornOn IWithName.WithName(string? name)
    {
        CreateStudent.withNameMethodInfo.Invoke(student, new object?[] { name });
        return this;
    }

    IInSemester IBornOn.BornOn(System.DateOnly? date)
    {
        CreateStudent.bornOnMethodInfo.Invoke(student, new object?[] { date });
        return this;
    }

    Student IInSemester.InSemester(int? semester)
    {
        CreateStudent.inSemesterMethodInfo.Invoke(student, new object?[] { semester });
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IBornOn WithName(string? name);
    }

    public interface IBornOn
    {
        IInSemester BornOn(System.DateOnly? date);
    }

    public interface IInSemester
    {
        Student InSemester(int? semester);
    }
}