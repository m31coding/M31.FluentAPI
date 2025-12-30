#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithOfOverloadedMethodClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteContinueWithOfOverloadedMethodClass()
    {
        var student = CreateStudent
            .Method1()
            .WithProperty1("Alice")
            .WithProperty2("King");

        Assert.Equal("Alice", student.Property1);
        Assert.Equal("King", student.Property2);
    }
}

#endif