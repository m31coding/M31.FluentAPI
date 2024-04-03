// Non-nullable member is uninitialized

#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.PersonClass;

[FluentApi]
public class Person
{
    [FluentMember(0)]
    public string FirstName { get; private set; }

    [FluentMember(1)]
    [FluentContinueWith(1)]
    public string? MiddleName { get; private set; }

    [FluentMember(1)]
    public string LastName { get; private set; }

    public string? HouseNumber { get; private set; }

    public string? Street { get; private set; }

    public string? City { get; private set; }

    public bool IsDigitalNomad { get; private set; }

    [FluentMethod(2)]
    [FluentBreak]
    private void WhoseAddressIsUnknown()
    {
    }

    [FluentMethod(2)]
    private void WhoLivesAtAddress()
    {
    }

    [FluentMethod(3)]
    private void WithHouseNumber(string houseNumber)
    {
        HouseNumber = houseNumber;
    }

    [FluentMethod(4)]
    private void WithStreet(string street)
    {
        Street = street;
    }

    [FluentMethod(5)]
    [FluentBreak]
    private void InCity(string city)
    {
        City = city;
    }

    [FluentMethod(2)]
    [FluentContinueWith(6)]
    private void WhoIsADigitalNomad()
    {
        IsDigitalNomad = true;
    }

    [FluentMethod(6)]
    private void LivingInCity(string city)
    {
        City = city;
    }
}