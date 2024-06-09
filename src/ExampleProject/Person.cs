// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class Person
{
    [FluentMember(0)]
    public string FirstName { get; private set; }

    [FluentMember(1)]
    [FluentSkippable]
    public string? MiddleName { get; private set; }

    [FluentMember(2)]
    public string LastName { get; private set; }

    public string? HouseNumber { get; private set; }

    public string? Street { get; private set; }

    public string? City { get; private set; }

    public bool IsDigitalNomad { get; private set; }

    [FluentMethod(3)]
    [FluentBreak]
    private void WhoseAddressIsUnknown()
    {
    }

    [FluentMethod(3)]
    private void WhoLivesAtAddress()
    {
    }

        [FluentMethod(4)]
        private void WithHouseNumber(string houseNumber)
        {
            HouseNumber = houseNumber;
        }

        [FluentMethod(5)]
        private void WithStreet(string street)
        {
            Street = street;
        }

        [FluentMethod(6)]
        [FluentBreak]
        private void InCity(string city)
        {
            City = city;
        }

    [FluentMethod(3)]
    [FluentContinueWith(7)]
    private void WhoIsADigitalNomad()
    {
        IsDigitalNomad = true;
    }

        [FluentMethod(7)]
        private void LivingInCity(string city)
        {
            City = city;
        }
}