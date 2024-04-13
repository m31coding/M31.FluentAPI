// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentDefaultMemberClass;

public class CreateStudent : CreateStudent.IBornOn
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static IBornOn WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Name = name;
        return createStudent;
    }

    public static IBornOn WithUnknownName()
    {
        CreateStudent createStudent = new CreateStudent();
        return createStudent;
    }

    public Student BornOn(System.DateOnly dateOfBirth)
    {
        student.DateOfBirth = dateOfBirth;
        return student;
    }

    public Student WithDefaultDateOfBirth()
    {
        return student;
    }

    public interface IBornOn
    {
        Student BornOn(System.DateOnly dateOfBirth);
        Student WithDefaultDateOfBirth();
    }
}