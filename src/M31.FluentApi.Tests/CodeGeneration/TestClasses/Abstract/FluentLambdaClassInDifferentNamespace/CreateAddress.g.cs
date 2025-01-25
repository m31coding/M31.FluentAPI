// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

namespace SomeOtherNamespace;

public class CreateAddress :
    CreateAddress.ICreateAddress,
    CreateAddress.IWithHouseNumber,
    CreateAddress.IWithStreet,
    CreateAddress.IInCity
{
    private readonly Address address;

    private CreateAddress()
    {
        address = new Address();
    }

    public static ICreateAddress InitialStep()
    {
        return new CreateAddress();
    }

    public static IWithStreet WithHouseNumber(string houseNumber)
    {
        CreateAddress createAddress = new CreateAddress();
        createAddress.address.HouseNumber = houseNumber;
        return createAddress;
    }

    IWithStreet IWithHouseNumber.WithHouseNumber(string houseNumber)
    {
        address.HouseNumber = houseNumber;
        return this;
    }

    IInCity IWithStreet.WithStreet(string street)
    {
        address.Street = street;
        return this;
    }

    Address IInCity.InCity(string city)
    {
        address.City = city;
        return address;
    }

    public interface ICreateAddress : IWithHouseNumber
    {
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
        Address InCity(string city);
    }
}