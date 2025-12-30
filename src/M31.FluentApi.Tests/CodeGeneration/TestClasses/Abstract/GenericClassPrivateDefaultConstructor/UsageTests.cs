#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassPrivateDefaultConstructor;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteGenericClassPrivateDefaultConstructorTest1()
    {
        var student = CreateStudent<int, string>
            .WithProperty1(22);

        Assert.Equal(22, student.Property1);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericClassPrivateDefaultConstructorTest2()
    {
        var student = CreateStudent<int, string>
            .WithProperty2("hello world");

        Assert.Equal("hello world", student.Property2);
    }
}

#endif