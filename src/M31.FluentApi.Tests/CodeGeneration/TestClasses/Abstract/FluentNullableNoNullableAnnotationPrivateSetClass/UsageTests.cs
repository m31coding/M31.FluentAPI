#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentNullableNoNullableAnnotationPrivateSetClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentNullableNoNullableAnnotationPrivateSetClass()
    {
        var student = CreateStudent
            .WithName("Alice");

        Assert.Equal("Alice", student.Name);
    }
}

#endif