#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaClass()
    {
        var student = CreateStudent
            .WithName("Alice")
            .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("23", student.Address.HouseNumber);
        Assert.Equal("Market Street", student.Address.Street);
        Assert.Equal("San Francisco", student.Address.City);
    }
}

#endif