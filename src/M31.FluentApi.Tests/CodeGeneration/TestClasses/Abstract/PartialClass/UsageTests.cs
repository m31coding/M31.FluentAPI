#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PartialClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecutePartialClass()
    {
        var student = CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("King", student.LastName);
    }
}

#endif