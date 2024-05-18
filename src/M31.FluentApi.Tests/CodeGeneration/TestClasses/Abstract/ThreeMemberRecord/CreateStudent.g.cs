// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ThreeMemberRecord;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IBornOn,
    CreateStudent.IInSemester
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

    public static IBornOn WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Name = name;
        return createStudent;
    }

    IBornOn IWithName.WithName(string name)
    {
        student.Name = name;
        return this;
    }

    IInSemester IBornOn.BornOn(System.DateOnly dateOfBirth)
    {
        student.DateOfBirth = dateOfBirth;
        return this;
    }

    Student IInSemester.InSemester(int semester)
    {
        student.Semester = semester;
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IBornOn WithName(string name);
    }

    public interface IBornOn
    {
        IInSemester BornOn(System.DateOnly dateOfBirth);
    }

    public interface IInSemester
    {
        Student InSemester(int semester);
    }
}