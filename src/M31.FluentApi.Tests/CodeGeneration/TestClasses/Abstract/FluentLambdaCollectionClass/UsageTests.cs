#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCollectionClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaCollectionClass()
    {
        var student = CreateStudent
            .WithName("Alice")
            .WithAddresses(
                a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"),
                a => a.WithHouseNumber("108").WithStreet("5th Avenue").InCity("New York"));

        Assert.Equal("Alice", student.Name);
        Assert.Equal(2, student.Addresses.Count);
        Assert.Equal("23", student.Addresses[0].HouseNumber);
        Assert.Equal("Market Street", student.Addresses[0].Street);
        Assert.Equal("San Francisco", student.Addresses[0].City);
        Assert.Equal("108", student.Addresses[1].HouseNumber);
        Assert.Equal("5th Avenue", student.Addresses[1].Street);
        Assert.Equal("New York", student.Addresses[1].City);
    }
}

#endif