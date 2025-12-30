#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithAfterCompoundClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteContinueWithAfterCompoundClass()
    {
        var student = CreateStudent
            .WithName("Alice", "King")
            .WithProperty2("Physics");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("King", student.LastName);
        Assert.Equal("Physics", student.Property2);
    }
}

#endif