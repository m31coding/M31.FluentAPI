// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#nullable enable

using M31.FluentApi.Attributes;
using System;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.PersonClass;

public class CreatePerson : CreatePerson.IWithMiddleNameWithLastName, CreatePerson.IWhoseAddressIsUnknownWhoLivesAtAddressWhoIsADigitalNomad, CreatePerson.IWithHouseNumber, CreatePerson.IWithStreet, CreatePerson.IInCity, CreatePerson.ILivingInCity
{
    private readonly Person person;
    private static readonly PropertyInfo firstNamePropertyInfo;
    private static readonly PropertyInfo middleNamePropertyInfo;
    private static readonly PropertyInfo lastNamePropertyInfo;
    private static readonly MethodInfo whoseAddressIsUnknownMethodInfo;
    private static readonly MethodInfo whoLivesAtAddressMethodInfo;
    private static readonly MethodInfo withHouseNumberMethodInfo;
    private static readonly MethodInfo withStreetMethodInfo;
    private static readonly MethodInfo inCityMethodInfo;
    private static readonly MethodInfo whoIsADigitalNomadMethodInfo;
    private static readonly MethodInfo livingInCityMethodInfo;

    static CreatePerson()
    {
        firstNamePropertyInfo = typeof(Person).GetProperty("FirstName", BindingFlags.Instance | BindingFlags.Public)!;
        middleNamePropertyInfo = typeof(Person).GetProperty("MiddleName", BindingFlags.Instance | BindingFlags.Public)!;
        lastNamePropertyInfo = typeof(Person).GetProperty("LastName", BindingFlags.Instance | BindingFlags.Public)!;
        whoseAddressIsUnknownMethodInfo = typeof(Person).GetMethod(
            "WhoseAddressIsUnknown",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] {  })!;
        whoLivesAtAddressMethodInfo = typeof(Person).GetMethod(
            "WhoLivesAtAddress",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] {  })!;
        withHouseNumberMethodInfo = typeof(Person).GetMethod(
            "WithHouseNumber",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(string) })!;
        withStreetMethodInfo = typeof(Person).GetMethod(
            "WithStreet",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(string) })!;
        inCityMethodInfo = typeof(Person).GetMethod(
            "InCity",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(string) })!;
        whoIsADigitalNomadMethodInfo = typeof(Person).GetMethod(
            "WhoIsADigitalNomad",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] {  })!;
        livingInCityMethodInfo = typeof(Person).GetMethod(
            "LivingInCity",
            BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[] { typeof(string) })!;
    }

    private CreatePerson()
    {
        person = new Person();
    }

    public static IWithMiddleNameWithLastName WithFirstName(string firstName)
    {
        CreatePerson createPerson = new CreatePerson();
        firstNamePropertyInfo.SetValue(createPerson.person, firstName);
        return createPerson;
    }

    public IWithMiddleNameWithLastName WithMiddleName(string? middleName)
    {
        middleNamePropertyInfo.SetValue(person, middleName);
        return this;
    }

    public IWhoseAddressIsUnknownWhoLivesAtAddressWhoIsADigitalNomad WithLastName(string lastName)
    {
        lastNamePropertyInfo.SetValue(person, lastName);
        return this;
    }

    public Person WhoseAddressIsUnknown()
    {
        whoseAddressIsUnknownMethodInfo.Invoke(person, new object?[] {  });
        return person;
    }

    public IWithHouseNumber WhoLivesAtAddress()
    {
        whoLivesAtAddressMethodInfo.Invoke(person, new object?[] {  });
        return this;
    }

    public ILivingInCity WhoIsADigitalNomad()
    {
        whoIsADigitalNomadMethodInfo.Invoke(person, new object?[] {  });
        return this;
    }

    public IWithStreet WithHouseNumber(string houseNumber)
    {
        withHouseNumberMethodInfo.Invoke(person, new object?[] { houseNumber });
        return this;
    }

    public IInCity WithStreet(string street)
    {
        withStreetMethodInfo.Invoke(person, new object?[] { street });
        return this;
    }

    public Person InCity(string city)
    {
        inCityMethodInfo.Invoke(person, new object?[] { city });
        return person;
    }

    public Person LivingInCity(string city)
    {
        livingInCityMethodInfo.Invoke(person, new object?[] { city });
        return person;
    }

    public interface IWithMiddleNameWithLastName
    {
        IWithMiddleNameWithLastName WithMiddleName(string? middleName);
        IWhoseAddressIsUnknownWhoLivesAtAddressWhoIsADigitalNomad WithLastName(string lastName);
    }

    public interface IWhoseAddressIsUnknownWhoLivesAtAddressWhoIsADigitalNomad
    {
        Person WhoseAddressIsUnknown();
        IWithHouseNumber WhoLivesAtAddress();
        ILivingInCity WhoIsADigitalNomad();
    }

    public interface IWithHouseNumber
    {
        IWithStreet WithHouseNumber(string houseNumber);
    }

    public interface IWithStreet
    {
        IInCity WithStreet(string street);
    }

    public interface IInCity
    {
        Person InCity(string city);
    }

    public interface ILivingInCity
    {
        Person LivingInCity(string city);
    }
}