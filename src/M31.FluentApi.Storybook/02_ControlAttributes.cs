// Non-nullable member is uninitialized
#pragma warning disable CS8618

// ReSharper disable CheckNamespace

using System.Text.Json;
using M31.FluentApi.Attributes;

namespace PersonExample1
{
    /* Control attributes allow you to jump between builder steps and finalize the object at desired stages. This
       enables the creation of non-linear paths and calling different methods based on previously invoked methods.
       Consider the person model below. The address properties are nullable and will only be set if available. We want
       to distinguish three different cases:

       1. The address is unknown; no address properties are set.
       2. The address is known; all address properties are set.
       3. The person is a digital nomad; only the city property is set. */

    [FluentApi]
    public class Person
    {
        public string Name { get; private set; }

        public string? HouseNumber { get; private set; }

        public string? Street { get; private set; }

        public string? City { get; private set; }

        public bool IsDigitalNomad { get; private set; }
    }
}

namespace PersonExample2
{
    /* Let's create the builder methods 'WhoseAddressIsUnknown', 'WhoLivesAtAddress' and 'WhoIsADigitalNomad' to handle
       the three different cases. To create a fork, all these methods are at builder step 1. After calling
       'WhoseAddressIsUnknown', the fluent builder should stop, which can be conveniently achieved by adding the
       attribute FluentBreak.
       When 'WhoLivesAtAddress' is called, the builder continues with steps 2, 3, and 4, setting the three address
       properties HouseNumber, Street, and City, respectively. I have indented the respective builder methods
       for better readability.
       So far, so good. However, for the generated API in this example, we also need to set the full address when
       'WhoIsADigitalNomad' is called. To complete our implementation, we must jump to step 5 in this case, as
       demonstrated in the next example. */

    [FluentApi]
    public class Person
    {
        [FluentMember(0)]
        public string Name { get; private set; }

        public string? HouseNumber { get; private set; }

        public string? Street { get; private set; }

        public string? City { get; private set; }

        public bool IsDigitalNomad { get; private set; }

        [FluentMethod(1)]
        [FluentBreak]
        private void WhoseAddressIsUnknown()
        {
        }

        [FluentMethod(1)]
        private void WhoLivesAtAddress()
        {
        }

            [FluentMethod(2)]
            private void WithHouseNumber(string houseNumber)
            {
                HouseNumber = houseNumber;
            }

            [FluentMethod(3)]
            private void WithStreet(string street)
            {
                Street = street;
            }

            [FluentMethod(4)]
            private void InCity(string city)
            {
                City = city;
            }

        [FluentMethod(1)]
        private void WhoIsADigitalNomad()
        {
            IsDigitalNomad = true;
        }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Person person1 = CreatePerson.WithName("Alice").WhoseAddressIsUnknown();
            Person person2 = CreatePerson.WithName("Bob").WhoLivesAtAddress()
                .WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco");
            Person person3 = CreatePerson.WithName("Eve").WhoIsADigitalNomad()
                .WithHouseNumber("82").WithStreet("Friedrichstraße").InCity("Berlin");
        }
    }
}

namespace PersonExample3
{
    /* The attribute FluentContinueWith indicates a jump to the specified builder step. In this example, when
       'WhoIsADigitalNomad' at step 1 is called, the fluent API continues with step 5, 'LivingInCity'. After step 5 the
       builder stops, since there are no further steps.
       With the introduction of step 5, the builder no longer stops after the 'InCity' method at step 4. To address
       this, I've added the FluentBreak attribute to the 'InCity' method. */

    [FluentApi]
    public class Person
    {
        [FluentMember(0)]
        public string Name { get; private set; }

        public string? HouseNumber { get; private set; }

        public string? Street { get; private set; }

        public string? City { get; private set; }

        public bool IsDigitalNomad { get; private set; }

        [FluentMethod(1)]
        [FluentBreak]
        private void WhoseAddressIsUnknown()
        {
        }

        [FluentMethod(1)]
        private void WhoLivesAtAddress()
        {
        }

            [FluentMethod(2)]
            private void WithHouseNumber(string houseNumber)
            {
                HouseNumber = houseNumber;
            }

            [FluentMethod(3)]
            private void WithStreet(string street)
            {
                Street = street;
            }

            [FluentMethod(4)]
            [FluentBreak]
            private void InCity(string city)
            {
                City = city;
            }

        [FluentMethod(1)]
        [FluentContinueWith(5)]
        private void WhoIsADigitalNomad()
        {
            IsDigitalNomad = true;
        }

            [FluentMethod(5)]
            private void LivingInCity(string city)
            {
                City = city;
            }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Person person1 = CreatePerson.WithName("Alice").WhoseAddressIsUnknown();
            Person person2 = CreatePerson.WithName("Bob").WhoLivesAtAddress()
                .WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco");
            Person person3 = CreatePerson.WithName("Eve").WhoIsADigitalNomad().LivingInCity("Berlin");
        }
    }
}

namespace PersonExample4
{
    /* This example demonstrates how the FluentContinueWith attribute can be used to create optional builder
       methods. To make the middle name optional, both the middle name and last name builder methods are specified at
       the same step, step 1. Additionally, the middle name builder method continues with step 1.
       With this approach, the generated API offers the desired behaviour: After setting the first name, you can either
       set the last name to complete the instance or set the middle name followed by the last name. */

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
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Person person2 = CreatePerson.WithFirstName("Bob").WithLastName("Bishop");
            Person person1 = CreatePerson.WithFirstName("Alice").WithMiddleName("Sophia").WithLastName("King");
        }
    }
}

namespace PersonExample5
{
    /* Lastly, the FluentReturn attribute, which can only be used on methods, instructs the builder to return the value
       produced by the decorated method. Without FluentReturn, FluentMethods must return void and the builder always
       returns the interface of the next step, or, in the final step, the built instance (e.g. a Person object).
       FluentReturn allows the builder to respect the return value of the decorated method, enabling the return of
       arbitrary types and values within the generated API. If a void method is decorated with FluentReturn, the builder
       method will also return void. */

    [FluentApi]
    public class Person
    {
        [FluentMember(0)]
        public string Name { get; private set; }

        [FluentMethod(1)]
        [FluentReturn]
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            string serializedPerson = CreatePerson.WithName("Alice King").ToJson();
        }
    }
}