#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableFirstMemberClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteSkippableFirstMemberClassTest1()
    {
        Student student = CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("King", student.LastName);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableFirstMemberClassTest2()
    {
        Student student = CreateStudent
            .WithLastName("King");

        Assert.Null(student.FirstName);
        Assert.Equal("King", student.LastName);
    }
}

#endif