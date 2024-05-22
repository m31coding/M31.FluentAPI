// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using M31.FluentApi.Attributes;
using System;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IWithAddress
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

    public static IWithAddress WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Name = name;
        return createStudent;
    }

    IWithAddress IWithName.WithName(string name)
    {
        student.Name = name;
        return this;
    }

    Student IWithAddress.WithAddress(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass.Address> createAddress)
    {
        student.Address = createAddress(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass.CreateAddress.InitialStep());
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IWithAddress WithName(string name);
    }

    public interface IWithAddress
    {
        Student WithAddress(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass.Address> createAddress);
    }
}