#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableMemberClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteSkippableMemberClassTest1()
    {
        Student student = CreateStudent
            .WithFirstName("Alice")
            .WithMiddleName("Sophia")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("Sophia", student.MiddleName);
        Assert.Equal("King", student.LastName);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableMemberClassTest2()
    {
        Student student = CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Null(student.MiddleName);
        Assert.Equal("King", student.LastName);
    }
}

#endif