#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaCompoundClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaCompoundClass()
    {
        var student = CreateStudent
            .WithName("Alice")
            .WithDetails(
                a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"),
                p => p.WithNumber("222-222-2222").WithUsage("CELL"));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("23", student.Address.HouseNumber);
        Assert.Equal("Market Street", student.Address.Street);
        Assert.Equal("San Francisco", student.Address.City);
        Assert.Equal("222-222-2222", student.Phone.Number);
        Assert.Equal("CELL", student.Phone.Usage);
    }
}

#endif