#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaRecursiveClass()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .WithFriend(f => f
                .WithName("Bob")
                .WithFriend(f2 => f2
                    .WithName("Eve")
                    .WithoutFriend()));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("Bob", student.Friend!.Name);
        Assert.Equal("Eve", student.Friend!.Friend!.Name);
        Assert.Null(student.Friend!.Friend!.Friend);
    }
}

#endif