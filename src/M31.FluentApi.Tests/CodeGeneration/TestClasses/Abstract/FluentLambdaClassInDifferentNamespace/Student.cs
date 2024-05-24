// Non-nullable member is uninitialized

#pragma warning disable CS8618

using M31.FluentApi.Attributes;
using SomeOtherNamespace;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClassInDifferentNamespace
{
    [FluentApi]
    public class Student
    {
        [FluentMember(0)]
        public string Name { get; set; }

        [FluentLambda(1)]
        public Address Address { get; set; }
    }
}

namespace SomeOtherNamespace
{
    [FluentApi]
    public class Address
    {
        [FluentMember(0)]
        public string Street { get; set; }

        [FluentMember(1)]
        public string HouseNumber { get; set; }
    }
}