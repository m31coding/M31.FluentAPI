// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IWithFriend
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

    public static IWithFriend WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Name = name;
        return createStudent;
    }

    IWithFriend IWithName.WithName(string name)
    {
        student.Name = name;
        return this;
    }

    Student IWithFriend.WithFriend(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.Student? friend)
    {
        student.Friend = friend;
        return student;
    }

    Student IWithFriend.WithFriend(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.CreateStudent.ICreateStudent, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.Student?> createFriend)
    {
        student.Friend = createFriend(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.CreateStudent.InitialStep());
        return student;
    }

    Student IWithFriend.WithoutFriend()
    {
        student.Friend = null;
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IWithFriend WithName(string name);
    }

    public interface IWithFriend
    {
        Student WithFriend(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.Student? friend);

        Student WithFriend(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.CreateStudent.ICreateStudent, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass.Student?> createFriend);

        Student WithoutFriend();
    }
}