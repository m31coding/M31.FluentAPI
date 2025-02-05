// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWithName,
    CreateStudent.IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG
{
    private readonly Student student;
    private static readonly PropertyInfo namePropertyInfo;
    private static readonly PropertyInfo addressesAPropertyInfo;
    private static readonly PropertyInfo addressesBPropertyInfo;
    private static readonly PropertyInfo addressesCPropertyInfo;
    private static readonly PropertyInfo addressesDPropertyInfo;
    private static readonly PropertyInfo addressesEPropertyInfo;
    private static readonly PropertyInfo addressesFPropertyInfo;
    private static readonly PropertyInfo addressesGPropertyInfo;

    static CreateStudent()
    {
        namePropertyInfo = typeof(Student).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public)!;
        addressesAPropertyInfo = typeof(Student).GetProperty("AddressesA", BindingFlags.Instance | BindingFlags.Public)!;
        addressesBPropertyInfo = typeof(Student).GetProperty("AddressesB", BindingFlags.Instance | BindingFlags.Public)!;
        addressesCPropertyInfo = typeof(Student).GetProperty("AddressesC", BindingFlags.Instance | BindingFlags.Public)!;
        addressesDPropertyInfo = typeof(Student).GetProperty("AddressesD", BindingFlags.Instance | BindingFlags.Public)!;
        addressesEPropertyInfo = typeof(Student).GetProperty("AddressesE", BindingFlags.Instance | BindingFlags.Public)!;
        addressesFPropertyInfo = typeof(Student).GetProperty("AddressesF", BindingFlags.Instance | BindingFlags.Public)!;
        addressesGPropertyInfo = typeof(Student).GetProperty("AddressesG", BindingFlags.Instance | BindingFlags.Public)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG WithName(string name)
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.namePropertyInfo.SetValue(createStudent.student, name);
        return createStudent;
    }

    IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG IWithName.WithName(string name)
    {
        CreateStudent.namePropertyInfo.SetValue(student, name);
        return this;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesA(System.Collections.Generic.List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesA)
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, addressesA);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesA(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesA)
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(addressesA));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesA(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesA)
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(createAddressesA.Select(createAddressA => createAddressA(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()))));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressA(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressA)
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(1){ addressA });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressA(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressA)
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(1){ createAddressA(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesA()
    {
        CreateStudent.addressesAPropertyInfo.SetValue(student, new List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(0));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesB(System.Collections.Generic.IReadOnlyCollection<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesB)
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, addressesB);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesB(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesB)
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, addressesB);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesB(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesB)
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, createAddressesB.Select(createAddressB => createAddressB(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep())).ToArray());
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressB(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressB)
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ addressB });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressB(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressB)
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ createAddressB(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesB()
    {
        CreateStudent.addressesBPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[0]);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesC(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesC)
    {
        CreateStudent.addressesCPropertyInfo.SetValue(student, addressesC);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesC(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesC)
    {
        CreateStudent.addressesCPropertyInfo.SetValue(student, createAddressesC.Select(createAddressC => createAddressC(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep())).ToArray());
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressC(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressC)
    {
        CreateStudent.addressesCPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ addressC });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressC(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressC)
    {
        CreateStudent.addressesCPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ createAddressC(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesC()
    {
        CreateStudent.addressesCPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[0]);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesD(System.Collections.Generic.HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesD)
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, addressesD);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesD(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesD)
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, new HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(addressesD));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesD(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesD)
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, new HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(createAddressesD.Select(createAddressD => createAddressD(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()))));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressD(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressD)
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, new HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(1){ addressD });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressD(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressD)
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, new HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(1){ createAddressD(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesD()
    {
        CreateStudent.addressesDPropertyInfo.SetValue(student, new HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>(0));
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesE(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[]? addressesE)
    {
        CreateStudent.addressesEPropertyInfo.SetValue(student, addressesE);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesE(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[]? createAddressesE)
    {
        CreateStudent.addressesEPropertyInfo.SetValue(student, createAddressesE?.Select(createAddressE => createAddressE(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep())).ToArray());
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressE(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressE)
    {
        CreateStudent.addressesEPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ addressE });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressE(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressE)
    {
        CreateStudent.addressesEPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[1]{ createAddressE(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesE()
    {
        CreateStudent.addressesEPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[0]);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesF(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[] addressesF)
    {
        CreateStudent.addressesFPropertyInfo.SetValue(student, addressesF);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesF(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?>[] createAddressesF)
    {
        CreateStudent.addressesFPropertyInfo.SetValue(student, createAddressesF.Select(createAddressF => createAddressF(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep())).ToArray());
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressF(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address? addressF)
    {
        CreateStudent.addressesFPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[1]{ addressF });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressF(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?> createAddressF)
    {
        CreateStudent.addressesFPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[1]{ createAddressF(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesF()
    {
        CreateStudent.addressesFPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[0]);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesG(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[]? addressesG)
    {
        CreateStudent.addressesGPropertyInfo.SetValue(student, addressesG);
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressesG(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?>[]? createAddressesG)
    {
        CreateStudent.addressesGPropertyInfo.SetValue(student, createAddressesG?.Select(createAddressG => createAddressG(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep())).ToArray());
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressG(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address? addressG)
    {
        CreateStudent.addressesGPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[1]{ addressG });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithAddressG(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?> createAddressG)
    {
        CreateStudent.addressesGPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[1]{ createAddressG(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.InitialStep()) });
        return student;
    }

    Student IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG.WithZeroAddressesG()
    {
        CreateStudent.addressesGPropertyInfo.SetValue(student, new M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[0]);
        return student;
    }

    public interface ICreateStudent : IWithName
    {
    }

    public interface IWithName
    {
        IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG WithName(string name);
    }

    public interface IWithAddressesAWithAddressesBWithAddressesCWithAddressesDWithAddressesEWithAddressesFWithAddressesG
    {
        Student WithAddressesA(System.Collections.Generic.List<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesA);

        Student WithAddressesA(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesA);

        Student WithAddressesA(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesA);

        Student WithAddressA(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressA);

        Student WithAddressA(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressA);

        Student WithZeroAddressesA();

        Student WithAddressesB(System.Collections.Generic.IReadOnlyCollection<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesB);

        Student WithAddressesB(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesB);

        Student WithAddressesB(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesB);

        Student WithAddressB(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressB);

        Student WithAddressB(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressB);

        Student WithZeroAddressesB();

        Student WithAddressesC(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesC);

        Student WithAddressesC(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesC);

        Student WithAddressC(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressC);

        Student WithAddressC(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressC);

        Student WithZeroAddressesC();

        Student WithAddressesD(System.Collections.Generic.HashSet<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> addressesD);

        Student WithAddressesD(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[] addressesD);

        Student WithAddressesD(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[] createAddressesD);

        Student WithAddressD(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressD);

        Student WithAddressD(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressD);

        Student WithZeroAddressesD();

        Student WithAddressesE(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address[]? addressesE);

        Student WithAddressesE(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address>[]? createAddressesE);

        Student WithAddressE(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address addressE);

        Student WithAddressE(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address> createAddressE);

        Student WithZeroAddressesE();

        Student WithAddressesF(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[] addressesF);

        Student WithAddressesF(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?>[] createAddressesF);

        Student WithAddressF(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address? addressF);

        Student WithAddressF(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?> createAddressF);

        Student WithZeroAddressesF();

        Student WithAddressesG(params M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?[]? addressesG);

        Student WithAddressesG(params Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?>[]? createAddressesG);

        Student WithAddressG(M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address? addressG);

        Student WithAddressG(Func<M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.CreateAddress.ICreateAddress, M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass.Address?> createAddressG);

        Student WithZeroAddressesG();
    }
}