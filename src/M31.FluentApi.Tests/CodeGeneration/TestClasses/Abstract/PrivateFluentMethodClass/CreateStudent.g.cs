// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;
using M31.FluentApi.Attributes;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFluentMethodClass;

public class CreateStudent :
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
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(string) })!;
        bornOnMethodInfo = typeof(Student).GetMethod(
            "BornOn",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(System.DateOnly) })!;
        inSemesterMethodInfo = typeof(Student).GetMethod(
            "InSemester",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(int) })!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static IBornOn WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        withNameMethodInfo.Invoke(createStudent.student, new object?[] { name });
        return createStudent;
    }

    public IInSemester BornOn(System.DateOnly date)
    {
        bornOnMethodInfo.Invoke(student, new object?[] { date });
        return this;
    }

    public Student InSemester(int semester)
    {
        inSemesterMethodInfo.Invoke(student, new object?[] { semester });
        return student;
    }

    public interface IBornOn
    {
        IInSemester BornOn(System.DateOnly date);
    }

    public interface IInSemester
    {
        Student InSemester(int semester);
    }
}