#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System.Reflection;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithSelfClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteContinueWithSelfClass()
    {
        Student student = CreateStudent
            .WithFirstName("Alice")
            .WithMiddleName("Emma")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("Emma", student.MiddleName);
        Assert.Equal("King", student.LastName);
    }
}

#endif
// todo: check all usage tests. Some have the wrong method names.