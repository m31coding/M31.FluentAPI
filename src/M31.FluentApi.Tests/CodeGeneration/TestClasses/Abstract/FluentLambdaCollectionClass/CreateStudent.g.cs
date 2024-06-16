// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;
using System;
using System.Linq;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IWithAddresses
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

    public static IWithAddresses WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Name = name;
        return createStudent;
    }

    IWithAddresses IWithName.WithName(string name)
    {
        student.Name = name;
        return this;
    }

    Student IWithAddresses.WithAddresses(System.Collections.Generic.List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address> addresses)
    {
        student.Addresses = addresses;
        return student;
    }

    Student IWithAddresses.WithAddresses(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address[] addresses)
    {
        student.Addresses = new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>(addresses);
        return student;
    }

    Student IWithAddresses.WithAddresses(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>[] createAddresses)
    {
        student.Addresses = new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>(createAddresses.Select(createAddress => createAddress(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.InitialStep())));
        return student;
    }

    Student IWithAddresses.WithAddress(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address address)
    {
        student.Addresses = new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>(1){ address };
        return student;
    }

    Student IWithAddresses.WithAddress(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address> createAddress)
    {
        student.Addresses = new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>(1){ createAddress(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.InitialStep()) };
        return student;
    }

    Student IWithAddresses.WithZeroAddresses()
    {
        student.Addresses = new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>(0);
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IWithAddresses WithName(string name);
    }

    public interface IWithAddresses
    {
        Student WithAddresses(System.Collections.Generic.List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address> addresses);

        Student WithAddresses(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address[] addresses);

        Student WithAddresses(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address>[] createAddresses);

        Student WithAddress(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address address);

        Student WithAddress(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass.Address> createAddress);

        Student WithZeroAddresses();
    }
}