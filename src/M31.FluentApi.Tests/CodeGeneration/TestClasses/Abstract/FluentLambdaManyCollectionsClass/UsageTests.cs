#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaManyCollectionsClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaManyCollectionsClassTest1()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .WithAddressesE(createAddressesE: null);

        Assert.Equal("Alice", student.Name);
        Assert.Null(student.AddressesE);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaManyCollectionsClassTest2()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .WithAddressesF(createAddressesF: _ => null);

        Assert.Single(student.AddressesF);
        Assert.Null(student.AddressesF[0]);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaManyCollectionsClassTest3()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .WithAddressesG(createAddressesG: null);

        Assert.Equal("Alice", student.Name);
        Assert.Null(student.AddressesG);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaManyCollectionsClassTest4()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .WithAddressesG(createAddressesG: _ => null);

        Assert.Equal("Alice", student.Name);
        Assert.NotNull(student.AddressesG);
        Assert.Single(student.AddressesG!);
        Assert.Null(student.AddressesG![0]);
    }
}

#endif