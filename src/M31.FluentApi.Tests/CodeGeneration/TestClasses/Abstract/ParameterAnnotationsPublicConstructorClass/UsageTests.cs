#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ParameterAnnotationsPublicConstructorClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteParameterAnnotationsPublicConstructorClass()
    {
        var student = CreateStudent
            .WithProperty1("hello world")
            .WithProperty2(42);

        Assert.Equal("hello world", student.Property1);
        Assert.Equal(42, student.Property2);
    }
}

#endif