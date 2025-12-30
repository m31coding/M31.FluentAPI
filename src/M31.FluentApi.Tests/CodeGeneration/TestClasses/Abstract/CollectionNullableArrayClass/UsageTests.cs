#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionNullableArrayClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteCollectionNullableArrayClass()
    {
        var student = CreateStudent
            .WhoseFriendsAre("Alice", "Bob");

        Assert.Equal(["Alice", "Bob"], student.Friends);
    }
}

#endif